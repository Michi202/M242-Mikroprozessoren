## Einleitung HTTP (Hypertext Transfer Protocol)
***

> [⇧ **Home**](https://github.com/iotkitv3/intro)

**Anfrage:**

```js
								
GET /infotext.html HTTP/1.1
Host: www.example.net
```

- - -

**Antwort:**

```js
HTTP/1.1 200 OK
Server: Apache/1.3.29 (Unix) PHP/4.3.4
Content-Length: 123456 (Größe von infotext.html in Byte)
Content-Language: de (nach RFC 3282 sowie RFC 1766)
Connection: close
Content-Type: text/html

Nachrichtenrumpf
```
- - -

Das Hypertext Transfer Protocol (HTTP, englisch für Hypertext-Übertragungsprotokoll) ist ein **Protokoll zur Übertragung von Nachrichten und Daten**. Es wird hauptsächlich eingesetzt, um Webseiten (Hypertext-Dokumente) aus dem World Wide Web (WWW) in einen Webbrowser zu laden. Es ist jedoch nicht prinzipiell darauf beschränkt und auch als allgemeines Dateiübertragungsprotokoll sehr verbreitet.

Jede Nachricht besteht dabei aus zwei Teilen, dem [Nachrichtenkopf (englisch Message Header, kurz: Header oder auch HTTP-Header genannt)](http://de.wikipedia.org/wiki/Liste_der_HTTP-Headerfelder) und dem Nachrichtenrumpf (englisch Message Body, kurz: Body). Der Nachrichtenkopf enthält die **Anfragemethode** und Informationen über den Nachrichtenrumpf wie etwa verwendete Kodierungen oder den Inhaltstyp. Der Nachrichtenrumpf enthält die Nutzdaten (siehe HTML unten).

Von den Nachrichten gibt es zwei unterschiedliche Arten: die Anfrage (englisch Request) vom **Client an den Server** und die Antwort (englisch Response) als Reaktion darauf vom **Server zum Client**. Die mbed Board&#039;s können als HTTP Client oder als HTTP Server Eingesetzt werden.

#### HTTP-Anfragemethoden (nicht abschliessend)

*   **GET**: ist die gebräuchlichste Methode. Mit ihr wird eine Ressource (zum Beispiel eine HTML Datei) vom Server angefordert.
*   **POST**: schickt Daten zur weiteren Verarbeitung zum Server.
*   **PUT**: dient dazu eine Ressource (zum Beispiel eine Datei) auf einen Webserver hochzuladen.
*   **DELETE**: löscht die angegebene Ressource auf dem Server.

Heute ist **PUT**, ebenso wie **DELETE**, kaum implementiert. Beides erlangt jedoch mit dem [REST Programmierparadigma](http://de.wikipedia.org/wiki/Representational_State_Transfer) neue Bedeutung.

### Anwendungen 

*   Holen, Schreiben und Löschen von Daten und Dateien auf HTTP Servern.

### HTML (Nachrichtenrumpf) 

Die Hypertext Markup Language (engl. für Hypertext-Auszeichnungssprache), abgekürzt HTML, ist eine textbasierte [maschinenlesbare Sprache (markup language)](http://de.wikipedia.org/wiki/Auszeichnungssprache) zur Strukturierung digitaler Dokumente wie Texte mit Hyperlinks, Bildern und anderen Inhalten.

HTML-Dokumente sind die Grundlage des World Wide Web und werden von Webbrowsern dargestellt.

## (HTTP) Cloud Services

![](https://raw.githubusercontent.com/iotkitv3/intro/main/images/CloudServices.png)

- - -

In der Cloud stehen einen Reihe von Services zur Verfügung womit die Boards mit Steuerungsinformationen, wie Sonnen Auf- und Untergang, Wetterentwicklung etc. versorgt werden können.

[ProgrammableWeb](http://www.programmableweb.com/apis/directory) und [Public APIs](https://github.com/public-apis/public-apis) liefert eine Übersicht dieser Dienste.

### Anwendungen 

*   Intelligente Dämmerungsschaltung mit Berücksichtigung Sonnen Auf- und Untergang
*   Intelligente, Vorausschauende Heizung, z.B. Heizung nicht aktivieren, wenn Sonnenschein angekündigt ist
*   Vorbeugung von Unwetterschäden, z.B. durch Einfahren der Sonnenstoren bei aufkommenden Sturm

### Beispiele

* [Sunrise Sunset](#sunset-sunrise-json) - HTTP GET
* [JSON](#json-javascript-object-notation)
* [ThinkSpeak](#thingspeak) - HTTP POST
* [Workflow mit Node-RED](#workflow)

Weitere Beispiele, inkl. HTTPS, findet man auf [http://os.mbed.com/teams/sandbox/code/http-example/](http://os.mbed.com/teams/sandbox/code/http-example/).


## Sunset Sunrise (JSON)
***

> [⇧ **Nach oben**](#)

![](https://raw.githubusercontent.com/iotkitv3/intro/main/images/SunriseSunset.png)

- - -

Sunrise Sunset stellt ein API zur Verfügung, mittels dem die Sonnen Auf- und Untergangszeiten für einen bestimmten Ort abgefragt werden können.

**Links**

*   [Website](http://sunrise-sunset.org/)
*   [API Beschreibung](http://sunrise-sunset.org/api)
*   [Sonnen Auf- und Untergang für Zürich](http://sunrise-sunset.org/search?location=Z%C3%BCrich%2C+Schweiz)

### Beispiel: Abfrage für Zürich 

[http://api.sunrise-sunset.org/json?lat=47.3686498&amp;lng=8.5391825](http://api.sunrise-sunset.org/json?lat=47.3686498&lng=8.5391825)

```js
{"results":
   { "sunrise":"5:38:12 AM",
     "sunset":"5:31:12 PM",
     "solar_noon":"11:34:42 AM",
     "day_length":"11:53:00",
     "civil_twilight_begin":"5:07:47 AM",
     "civil_twilight_end":"6:01:38 PM",
     "nautical_twilight_begin":"4:32:04 AM",
     "nautical_twilight_end":"6:37:21 PM",
     "astronomical_twilight_begin":"3:55:32 AM",
     "astronomical_twilight_end":"7:13:52 PM"
   },
   "status":"OK"}
```

### Beispiel(e)

Das Beispiel [SunriseSunset](main.cpp) holt, mittels HTTP GET, die Informationen für Sonnenauf- und Untergang von http://sunrise-sunset.org.

## JSON (JavaScript Object Notation)
***

> [⇧ **Nach oben**](#)

```js
{ "my_array": [ "demo_string", 10], 
              "my_boolean": true }                              

```

Beispiel JSON Code

- - -

```js
label: [ string, int ],
          label: int              
```

Abbildung JSON Code im Speicher 

- - - 

Das Sunrise Sunset Beispiel verwendet, als Datenformat, die [JavaScript Object Notation, kurz JSON](http://de.wikipedia.org/wiki/JavaScript_Object_Notation).

Die [JavaScript Object Notation, kurz JSON](http://de.wikipedia.org/wiki/JavaScript_Object_Notation), ist ein kompaktes Datenformat in einer einfach lesbaren Textform zum Zweck des Datenaustauschs zwischen Anwendungen. Jedes gültige JSON-Dokument soll ein gültiges JavaScript sein und per eval() interpretiert werden können. JSON ist unabhängig von der Programmiersprache. Parser existieren in praktisch allen verbreiteten Sprachen.

Mittels der [MbedJSONValue](http://developer.mbed.org/users/samux/code/MbedJSONValue/) Library können JSON Strukturen geparst oder erzeugt werden.

### Beispiel(e)

Das Beispiel JSONParser erzeugt eine JSON Struktur und parst diese nach C++.

<details><summary>main.cpp</summary>  

    /** JSON Beispiel
    */
    #include "mbed.h"
    #include "MbedJSONValue.h"
    #include <string>
    
    int main()
    {
        // C++ to JSON
        MbedJSONValue demo;
        std::string s;
    
        //fill the object
        demo["my_array"][0] = "demo_string";
        demo["my_array"][1] = 10;
        demo["my_boolean"] = false;
    
        //serialize it into a JSON string
        s = demo.serialize();
        printf( "json: %s\r\n", s.c_str() );
    
        // JSON to C++
        const  char * json = "{\"my_array\": [\"demo_string\", 10], \"my_boolean\": true}";
    
        //parse the previous string and fill the object demo
        parse( demo, json );
    
        std::string my_str;
        int my_int;
        bool my_bool;
    
        my_str = demo["my_array"][0].get<std::string>();
        my_int = demo["my_array"][1].get<int>();
        my_bool = demo["my_boolean"].get<bool>();
    
        printf("my_str: %s\r\n", my_str.c_str());
        printf("my_int: %d\r\n", my_int);
        printf("my_bool: %s\r\n", my_bool ? "true" : "false");
    }
</p></details>


## ThingSpeak
***

> [⇧ **Nach oben**](#)


![](https://raw.githubusercontent.com/iotkitv3/intro/main/images/ThingSpeak.png)

- - -

ThingSpeak ist eine "Internet der Dinge" Anwendung um Daten zu sammeln, analysieren und mittels Triggern darauf zu reagieren.

Um ThingSpeak verwenden zu könnnen ist zuerst ein Login [SigUp](https://thingspeak.com/) zu lösen und anschliessend ein neuer Channel mit folgenden Feldern einzurichten:

*   Field 1: Temp
*   Field 2: Hum

Auf den **Data Import / Export** Tab zu wechseln und die Variablen _host_ und _key_ entsprechend den **Update Channel Feed - POST (rechts)** Angaben anzupassen.

Programm compilieren und auf das Board downloaden. Unter **Private View** sollten zwei Grafiken mit den aktuellen Sensorwerten sichtbar werden.

**Links**

*   [ThingSpeak Channel aus Beispiel](https://thingspeak.com/channels/82291) 
*   [Website](https://thingspeak.com/)
*   [Einführung in ThingSpeak](http://www.codeproject.com/Articles/845538/An-Introduction-to-ThingSpeak)
*   [ThingSpeak im Praxistest](http://blog.zuehlke.com/die-iot-plattform-thingspeak-im-praxistest/)

### Beispiel(e)

Das Beispiel ThingSpeak schickt, mittels HTTP POST, Sensordaten an den ThingSpeak Cloud Dienst. Die Daten sind dann auf folgendem [ThingSpeak Channel](https://thingspeak.com/channels/82291) ersichtlich.

<details><summary>main.cpp</summary>  


    /** Beispiel Senden von Sensordaten an ThingSpeak
        */
    #include "mbed.h"
    #if MBED_CONF_IOTKIT_HTS221_SENSOR == true
    #include "HTS221Sensor.h"
    #endif
    #if MBED_CONF_IOTKIT_BMP180_SENSOR == true
    #include "BMP180Wrapper.h"
    #endif
    #include "http_request.h"
    #include "OLEDDisplay.h"
    
    // UI
    OLEDDisplay oled( MBED_CONF_IOTKIT_OLED_RST, MBED_CONF_IOTKIT_OLED_SDA, MBED_CONF_IOTKIT_OLED_SCL );
    
    static DevI2C devI2c( MBED_CONF_IOTKIT_I2C_SDA, MBED_CONF_IOTKIT_I2C_SCL );
    #if MBED_CONF_IOTKIT_HTS221_SENSOR == true
    static HTS221Sensor hum_temp(&devI2c);
    #endif
    #if MBED_CONF_IOTKIT_BMP180_SENSOR == true
    static BMP180Wrapper hum_temp( &devI2c );
    #endif
    
    /** ThingSpeak URL und API Key ggf. anpassen */
    char host[] = "http://api.thingspeak.com/update";
    char key[] = "A2ABBMDJYRAMA6JM";
    
    // I/O Buffer
    char message[1024];
    
    DigitalOut myled(MBED_CONF_IOTKIT_LED1);
    
    int main()
    {
        uint8_t id;
        float value1, value2;
    
        printf("\tThingSpeak\n");
    
        /* Init all sensors with default params */
        hum_temp.init(NULL);
        hum_temp.enable();
    
        hum_temp.read_id(&id);
        printf("HTS221  humidity & temperature    = 0x%X\r\n", id);
    
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
            hum_temp.get_temperature(&value1);
            hum_temp.get_humidity(&value2);
    
            sprintf( message, "%s?key=%s&field1=%f&field2=%f", host, key, value1, value2 );
            printf( "%s\n", message );
            oled.cursor( 1, 0 );
            oled.printf( "temp: %3.2f\nhum : %3.2f", value1, value2 );
    
            myled = 1;
            HttpRequest* get_req = new HttpRequest( network, HTTP_POST, message );
    
            HttpResponse* get_res = get_req->send();
            if (!get_res)
            {
                printf("HttpRequest failed (error code %d)\n", get_req->get_error());
                return 1;
            }
            delete get_req;
            myled = 0;
            thread_sleep_for( 10000 );
        }
    }

</p></details>

## Workflow
***

> [⇧ **Nach oben**](#)

![](https://raw.githubusercontent.com/iotkitv3/intro/main/images/NodeRED.png)

- - -

Mittels [Node-RED](https://nodered.org/) lassen sich einfache Workflows realisieren und die Protokolle testen.

### Node-RED HTTP Workflow

![](https://raw.githubusercontent.com/iotkitv3/intro/main/images/NodeREDHTTP.png)

- - -

* Benötigte Software installieren (z.B. auf einem Raspberry Pi oder einer Linux VM)
    * [Node-RED](https://nodered.org/) - Workflow Engine.
    * [ngrok](https://ngrok.com/) für eine Public URL. Wenn z.B. der Raspberry Pi hinter einer Firewall ist.
* In Node-RED
    * `http` Input Node auf Flow 1 platzieren, mit als Methode `POST` und als URL `post` eintragen.
    * `debug` Output Node auf Flow 1 platzieren, Output auf "complete msg.object" ändern und mit Input Node verbinden.
    * Programm mittels `Deploy` veröffentlichen.
* mbed Teil
    * [HTTP ThingSpeak](#thingspeak) Beispiel (main.cpp) editieren, ca. auf Zeile 16 die URL mit dem Server ersetzen wo Node-RED läuft, z.B. `http://192.168.178.200:1880/post`.
    * Programm Compilieren und auf Board laden.

Im Node-RED Fenster auf der Node `debug`, sollten jetzt alle 10 Sekunden neue Meldungen vom IoTKitV3 erscheinen.  

**Links**
 
 * [Home Page](https://nodered.org/)
 * [Node-RED Einführung](https://www.youtube.com/watch?v=f5o4tIz2Zzc)
