/** MQTT Publish von Sensordaten */
#include "mbed.h"
#include "OLEDDisplay.h"
#include "Motor.h"
#include "Servo.h"

#if MBED_CONF_IOTKIT_HTS221_SENSOR == true
#include "HTS221Sensor.h"
#endif
#if MBED_CONF_IOTKIT_BMP180_SENSOR == true
#include "BMP180Wrapper.h"
#endif
//#include "MFRC522.h"

#ifdef TARGET_K64F
#include "QEI.h"

//Use X2 encoding by default.
QEI wheel (MBED_CONF_IOTKIT_BUTTON2, MBED_CONF_IOTKIT_BUTTON3, NC, 624);
#endif

#include <MQTTNetwork.h>
#include <MQTTClient.h>
#include <MQTTmbed.h> 

// Sensoren wo Daten fuer Topics produzieren
static DevI2C devI2c( MBED_CONF_IOTKIT_I2C_SDA, MBED_CONF_IOTKIT_I2C_SCL );
#if MBED_CONF_IOTKIT_HTS221_SENSOR == true
static HTS221Sensor hum_temp(&devI2c);
#endif
#if MBED_CONF_IOTKIT_BMP180_SENSOR == true
static BMP180Wrapper hum_temp( &devI2c );
#endif
AnalogIn hallSensor( MBED_CONF_IOTKIT_HALL_SENSOR );
DigitalIn button( MBED_CONF_IOTKIT_BUTTON1 );

// Topic's publish
char* topicTEMP = (char*) "iotkitnoah/noahsensor";
// Topic's subscribe
char* topicActors = (char*) "iotkitnoah/actors/#";
// MQTT Brocker
char* hostname = (char*) "cloud.tbz.ch";
int port = 1883;
// MQTT Message
MQTT::Message message;
// I/O Buffer
char buf[100];

// Klassifikation 
char cls[3][10] = { "low", "middle", "high" };
int type = 0;

// UI
OLEDDisplay oled( MBED_CONF_IOTKIT_OLED_RST, MBED_CONF_IOTKIT_OLED_SDA, MBED_CONF_IOTKIT_OLED_SCL );
DigitalOut alert( MBED_CONF_IOTKIT_LED3 );

// Aktore(n)
PwmOut speaker( MBED_CONF_IOTKIT_BUZZER );
Motor m1( MBED_CONF_IOTKIT_MOTOR2_PWM, MBED_CONF_IOTKIT_MOTOR2_FWD, MBED_CONF_IOTKIT_MOTOR2_REV ); // PWM, Vorwaerts, Rueckwarts
// NFC/RFID Reader (SPI)
//MFRC522    rfidReader( MBED_CONF_IOTKIT_RFID_MOSI, MBED_CONF_IOTKIT_RFID_MISO, MBED_CONF_IOTKIT_RFID_SCLK, MBED_CONF_IOTKIT_RFID_SS, MBED_CONF_IOTKIT_RFID_RST ); 
// Servo2 (Pin mit PWM, K64F = D11, andere D9)
#ifdef TARGET_K64F
Servo servo2 ( MBED_CONF_IOTKIT_SERVO3 );
#else
Servo servo2 ( MBED_CONF_IOTKIT_SERVO2 );
#endif

/** Hilfsfunktion zum Publizieren auf MQTT Broker */
void publish( MQTTNetwork &mqttNetwork, MQTT::Client<MQTTNetwork, Countdown> &client, char* topic )
{
    MQTT::Message message;    
    oled.cursor( 2, 0 );
    oled.printf( "Topi: %s\n", topic );
    oled.cursor( 3, 0 );    
    oled.printf( "Push: %s\n", buf );
    message.qos = MQTT::QOS0;
    message.retained = false;
    message.dup = false;
    message.payload = (void*) buf;
    message.payloadlen = strlen(buf)+1;
    client.publish( topic, message);  
}

/** Daten empfangen von MQTT Broker */
void messageArrived( MQTT::MessageData& md )
{
    float value;
    MQTT::Message &message = md.message;
    printf("Message arrived: qos %d, retained %d, dup %d, packetid %d\n", message.qos, message.retained, message.dup, message.id);
    printf("Topic %.*s, ", md.topicName.lenstring.len, (char*) md.topicName.lenstring.data );
    // MQTT schickt kein \0, deshalb manuell anfuegen
    ((char*) message.payload)[message.payloadlen] = '\0';
    printf("Payload %s\n", (char*) message.payload);

    // Aktoren
    if  ( strncmp( (char*) md.topicName.lenstring.data + md.topicName.lenstring.len - 6, "servo2", 6) == 0 )
    {
        sscanf( (char*) message.payload, "%f", &value );
        servo2 = value;
        printf( "Servo2 %f\n", value );
    }               
}

/** Hauptprogramm */
int main()
{
    uint8_t id;
    float temp, hum;
    int encoder;
    alert = 0;
    servo2 = 0.5f;
    
    oled.clear();
    oled.printf( "MQTTPublish\r\n" );
    oled.printf( "host: %s:%s\r\n", hostname, port );

    printf("\nConnecting to %s...\n", MBED_CONF_APP_WIFI_SSID);
    oled.printf( "SSID: %s\r\n", MBED_CONF_APP_WIFI_SSID );
    
    // Connect to the network with the default networking interface
    // if you use WiFi: see mbed_app.json for the credentials
    WiFiInterface *wifi = WiFiInterface::get_default_instance();
    if ( !wifi ) 
    {
        printf("ERROR: No WiFiInterface found.\n");
        return -1;
    }
    printf("\nConnecting to %s...\n", MBED_CONF_APP_WIFI_SSID);
    int ret = wifi->connect( MBED_CONF_APP_WIFI_SSID, MBED_CONF_APP_WIFI_PASSWORD, NSAPI_SECURITY_WPA_WPA2 );
    if ( ret != 0 ) 
    {
        printf("\nConnection error: %d\n", ret);
        return -1;
    }    

    // TCP/IP und MQTT initialisieren (muss in main erfolgen)
    MQTTNetwork mqttNetwork( wifi );
    MQTT::Client<MQTTNetwork, Countdown> client(mqttNetwork);

    printf("Connecting to %s:%d\r\n", hostname, port);
    int rc = mqttNetwork.connect(hostname, port);
    if (rc != 0)
        printf("rc from TCP connect is %d\r\n", rc); 

    // Zugangsdaten - der Mosquitto Broker ignoriert diese
    MQTTPacket_connectData data = MQTTPacket_connectData_initializer;
    data.MQTTVersion = 3;
    data.clientID.cstring = (char*) wifi->get_mac_address(); // muss Eindeutig sein, ansonsten ist nur 1ne Connection moeglich
    data.username.cstring = (char*) wifi->get_mac_address(); // User und Password ohne Funktion
    data.password.cstring = (char*) "password";
    if ((rc = client.connect(data)) != 0)
        printf("rc from MQTT connect is %d\r\n", rc);           

    // MQTT Subscribe!
    client.subscribe( topicActors, MQTT::QOS0, messageArrived );
    printf("MQTT subscribe %s\n", topicActors );
    
    /* Init all sensors with default params */
    hum_temp.init(NULL);
    hum_temp.enable();  

    while   ( 1 ) 
    {
        // Temperator und Luftfeuchtigkeit
        hum_temp.read_id(&id);
        hum_temp.get_temperature(&temp);
        hum_temp.get_humidity(&hum);    
        if  ( type == 0 )
        {
            temp -= 5.0f;
            m1.speed( 0.0f );
        }
        else if  ( type == 2 )
        {
            temp += 5.0f;
            m1.speed( 1.0f );
        }
        else
        {
            m1.speed( 0.75f );
        }
        sprintf( buf, "0x%X,%2.2f,%2.1f,%s", id, temp, hum, cls[type] ); 
        type++;
        if  ( type > 2 )
            type = 0;       
        
        publish( mqttNetwork, client, topicTEMP );      

#ifdef TARGET_K64F

        // Encoder
        encoder = wheel.getPulses();
        sprintf( buf, "%d", encoder );
        printf("before publish function");
        publish( mqttNetwork, client, topicENCODER );
#endif

        client.yield    ( 1000 );                   // MQTT Client darf empfangen
        thread_sleep_for( 500 );
    }

    // Verbindung beenden
    if ((rc = client.disconnect()) != 0)
        printf("rc from disconnect was %d\r\n", rc);

    mqttNetwork.disconnect();    
}
