# M242-Mikroprozessoren

![Analyse1](images/Analyse1.PNG)

Message Queue Telemetry Transport (MQTT) ist ein offenes Nachrichten-Protokoll für Machine-to-Machine-Kommunikation (M2M), das die Übertragung von Messdaten-Daten in Form von Nachrichten zwischen Geräten ermöglicht, trotz hoher Verzögerungen oder beschränkten Netzwerken.
Quelle:  iotkitv3 / mqtt (https://github.com/iotkitv3/mqtt) 

Unsere Aufgabe war es, unser Projekt das mittels HTTP und REST Daten über Sensoren aufnimmt und diese sendet, auszubauen das diese mittels MQTT möglich ist.


## Was macht das Projekt  
Unsere Projektidee besteht aus 3 unterschiedlichen Aspekten und simuliert die Sicherheitsvorkehrung einer Schleuse die zu extremen Temperaturen führt. 
Dabei liefern 2 IoT-Kits die benötigten Daten: Das eine liefert die Umliegende Temperatur, das andere die Informationen einer Keycard, die an den Sensor geführt wird. Würden Anomalien auftreten die zu sehr hohem Temperatur Unterschied führen würden zwischen den 2 IoT-Kits, würde der Zugang mit der Keycard verweigert werden.

## Quick Start

Die Installation ist einfach, das Projekt zu sich herunterladen via git clone.
```git clone https://github.com/Michi202/M242-Mikroprozessoren.git```

Danach ```MBED Studio``` öffnen und in das Repository navigieren.

<img src="https://github.com/Michi202/M242-Mikroprozessoren/blob/LB03/images/OpenWorkspace.png" width=200 height=300>

Dann muss das zu ausführende Programm ausgewählt werden und das korrekte Modell ausgewählt werden für das IoT-Kit. Beim ausführen braucht dies einige Zeit zum kompilieren.

<img src="https://github.com/Michi202/M242-Mikroprozessoren/blob/LB03/images/ActiveProgram.png" width=350 height=300>

Die Seite wird via localhost gehostet.
Alle Geräte müssen sich im selben Netzwerk befinden und die IP-Adresse des WebApp-Gerätes muss den Kits bekannt sein.
Unter localhost:8080 können sie die Seite aufrufen.

## References  

### Send NFC
[SendNFC](SendNFC/README.md) 

#### Konzept

Das SendNFC-Programm basiert auf dem von der Lehrperson zu Verfügung gestelltem HTTP-Programm. Wenn ein neues NFC Karte/Batch erkannt wird, wird diese mittels HTTP_Request an eine vordefinierte Schnittstelle gesendet.

Die UID wird am IoT-Kit mittels eines Sensors erkannt und in der Request als JSON-Objekt dem Empfänger zugestellt. Die übermittelte UID wir im Backend verglichen, falls ein User existiert, und eine Berechtigung hat, wird der Zugriff erlaubt, ansonsten wird der Zugriff verweigert.

#### Probleme

Meine Herausforderung war grösstens, das knowhow am der Sprache. C++ war neuland für mich. Ich habe versucht mich findig zu machen unm zu verstehen, wie dies ungefär funktoniert. Mit hilfe der verschiedenen Repos auf git, welche wir von unserem Lehrer zu verfügung gestellt bekommen haben, konnte ich mich gut orientiren. Danach ging alles soweit gut ab.

#### Anwendung

Um das Programm auf einem IoT-Kit anzuwenden, dieses einfach an denn Rechner anschliessen. MBED-Studio erkennt das KIT automatisch und führt das Programm über dieses aus. Anschliessend muss man eine NFC-Karte an unseren Sensonr halten, damitt es los gehen kann!


[sendTemperature](sendTemperature/README.md)  
[WebApp](WebApp/README.md)  

## Ablauf
Der User begibt sich auf das Eingabeformular und gibt seine Login-Daten ein. Danach wird er dazu aufgefordert, seine Key-Card an den Sensor zu halten. Ist die Temperatur auserhalb des vorgegebenen Limits, so erlaubt ihm der Login keinen Zugriff. Ist die Temperatur jedoch passabel, so wird er auf eine Seite weitergeleitet, die unterschiedliche Statistiken anzeigt (anzahl Logins, Temperaturen, usw.). Im "ernsten" Anwendungsfall würde sich dann die Türe öffnen.

## Aufteilung
Da Severin Senn sich mit C# und vue.js mehr auf Seiten WebApp auskennt, hat er diese Aufgabe übernommen, wärend der Rest sich mit C++ und den IoT-Kits auseinandergesetzt hat. Jedoch haben wir uns auch untereinander Ausgeholfen und gemeinsam schwierigere Probleme behoben, sei es bei den Kits oder der WebApp.

## Demo
![Analyse1](images/Analyse1.PNG)
![Analyse](images/Analyse.PNG)
![NFCScan](images/NFCScan.PNG)
![ZuHoheTemps](images/ZuHoheTemps.PNG)
