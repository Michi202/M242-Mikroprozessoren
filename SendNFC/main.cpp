/** Beispiel Abfrage Cloud Dienst Sunrise / Sunset
 */
#include "mbed.h"
#include <string>
#include "MFRC522.h"
#include "OLEDDisplay.h"
#include "http_request.h"
#include "MbedJSONValue.h"


// UI
OLEDDisplay oled( MBED_CONF_IOTKIT_OLED_RST, MBED_CONF_IOTKIT_OLED_SDA, MBED_CONF_IOTKIT_OLED_SCL );

// NFC/RFID Reader (SPI)
MFRC522    rfidReader( MBED_CONF_IOTKIT_RFID_MOSI, MBED_CONF_IOTKIT_RFID_MISO, MBED_CONF_IOTKIT_RFID_SCLK, MBED_CONF_IOTKIT_RFID_SS, MBED_CONF_IOTKIT_RFID_RST ); 


int main()
{
    // OLED Display
    oled.clear();
    oled.printf( "RFID\n" );
        
    rfidReader.PCD_Init();
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
    int count = 0;

    while   ( 1 ) 
    {
        // RFID Reader
        if ( rfidReader.PICC_IsNewCardPresent())
            if ( rfidReader.PICC_ReadCardSerial()) 
            {
                count = count + 1;
                char body[1024];
                oled.cursor( 1, 0 );                
                // Print Card UID (2-stellig mit Vornullen, Hexadecimal)
                string card_id = "";

                oled.printf("UID: ");
                for ( int i = 0; i < rfidReader.uid.size; i++ ){
                    oled.printf("%02X:", rfidReader.uid.uidByte[i]);
                    card_id = card_id + std::to_string(rfidReader.uid.uidByte[i]) + ":";
                }
                oled.printf("\r\n");

                HttpRequest* post_req = new HttpRequest( network, HTTP_POST, "http://192.168.104.27/M242/api/Home/SendNFC");
                post_req->set_header("Content-Type", "application/json");

                sprintf( body, "{ \"CardId\": \"%s\" }", card_id.c_str());
                HttpResponse* post_res = post_req->send(body, strlen(body));
                
                // Print Card type
                int piccType = rfidReader.PICC_GetType(rfidReader.uid.sak);
                oled.printf("PICC Type: %s \r\n", rfidReader.PICC_GetTypeName(piccType) );
                printf("Card Count: %d \r\n", count);
            }
        thread_sleep_for( 200 );
    }
}
