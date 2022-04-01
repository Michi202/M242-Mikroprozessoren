/** Beispiel Abfrage Cloud Dienst Sunrise / Sunset
 */
#include "mbed.h"
#include <cstdio>
#include <string>
#include "OLEDDisplay.h"
#include "HTS221Sensor.h"
#include "http_request.h"
#include "MbedJSONValue.h"

// UI
OLEDDisplay oled( MBED_CONF_IOTKIT_OLED_RST, MBED_CONF_IOTKIT_OLED_SDA, MBED_CONF_IOTKIT_OLED_SCL );
// I/O Buffer
static DevI2C devI2c( MBED_CONF_IOTKIT_I2C_SDA, MBED_CONF_IOTKIT_I2C_SCL );
static HTS221Sensor hum_temp(&devI2c);
char message[6000];

DigitalOut myled( MBED_CONF_IOTKIT_LED1 );

int main()
{
    oled.clear();

    hum_temp.init(NULL);
    hum_temp.enable();
    
    oled.printf("Sunrise Sunset\n");
    // Connect to the network with the default networking interface
    // if you use WiFi: see mbed_app.json for the credentials
    WiFiInterface* network = WiFiInterface::get_default_instance();
    if (!network) {
        printf("ERROR: No WiFiInterface found.\n");
        return -1;
    }

    printf("\nConnecting to %s...\n", MBED_CONF_APP_WIFI_SSID);
    int ret = network->connect(MBED_CONF_APP_WIFI_SSID, MBED_CONF_APP_WIFI_PASSWORD, NSAPI_SECURITY_WPA_WPA2);
    if (ret != 0) {
        printf("\nConnection error: %d\n", ret);
        return -1;
    }
    printf("Success\n\n");
    printf("MAC: %s\n", network->get_mac_address());
    SocketAddress a;
    network->get_ip_address(&a);
    printf("IP: %s\n", a.get_ip_address());    

    while( 1 )
    {
        myled = 1;
        // By default the body is automatically parsed and stored in a buffer, this is memory heavy.
        // To receive chunked response, pass in a callback as last parameter to the constructor.
        //TODO: Here starts Post
        float value1, value2;
        char body[1024];
        hum_temp.get_temperature(&value1);
        hum_temp.get_humidity(&value2);

        printf("temperature: %f",value1);
        printf("humidity: %f",value2);


        HttpRequest* post_req = new HttpRequest( network, HTTP_POST, "http://192.168.104.27/M242/api/Home/SendTemp");
        post_req->set_header("Content-Type", "application/json");

        sprintf( body, "{ \"Temperature\": %f, \"humidity\": %f }", value1, value2 );
        HttpResponse* post_res = post_req->send(body, strlen(body));
        // OK
        /*
        if ( get_res )
        {
            MbedJSONValue parser; i made changes
            // HTTP GET (JSON) parsen  
            parse( parser, get_res->get_body_as_string().c_str() );
            
            std::string sunrise;
            std::string sunset;            
            
            sunrise = parser["results"]["sunrise"].get<std::string>();
            sunset  = parser["results"]["sunset"] .get<std::string>(); 
            
            // Umwandlung nach int. Damit die Zeiten besser verglichen werden können.
            int rh, rm, rs, sh, sm, ss;
            sscanf( sunrise.c_str(), "%d:%d:%d AM", &rh, &rm, &rs );
            sscanf( sunset .c_str(), "%d:%d:%d PM", &sh, &sm, &ss );
            
            oled.cursor( 1, 0 );
            oled.printf( "auf   %02d.%02d.%02d\nunter %02d.%02d.%02d\n", rh+2, rm, rs, sh+2+12, sm, ss );
        }
        // Error
        else
        {
            printf("HttpRequest failed (error code %d)\n", get_req->get_error());
            return 1;
        } */
        delete post_req;
        myled = 0;

        thread_sleep_for( 10000 );
    }
}
