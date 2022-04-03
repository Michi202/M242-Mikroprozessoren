# M242-Mikroprozessoren

**Projektidee**

Unsere Projektidee besteht aus 3 unterschiedlichen Aspekten und simuliert die Sicherheitsvorkehrung einer Schleuse die zu Extrement Temperaturen führt. 
Dabei liefern 2 IoT-Kits die benötigten Daten: Das eine liefert die Umliegende Temperatur, das andere die Informationen einer Keycard, die an den Sensor geführt wird. 

**Ablauf**

Der User begibt sich auf das Eingabeformular und gibt seine Login-Daten ein. Danach wird er dazu aufgefordert, seine Key-Card an den Sensor zu halten. Ist die Temperatur auserhalb des vorgegebenen Limits, so erlaubt ihm der Login keinen Zugriff. Ist die Temperatur jedoch passabel, so wird er auf eine Seite weitergeleitet, die unterschiedliche Statistiken anzeigt (anzahl Logins, Temperaturen, usw.). Im "ernsten" Anwendungsfall würde sich dann die Türe öffnen.

**Aufsetzung**

Um das ganze Projekt durchzuführen muss man die WebApp via Localhost hosten. Die Programme zum senden von Temperatur und Keycard-infos müssen jeweils auf einem IoT-Kit ausgeführt werden. Alle Geräte müssen sich im selben Netzwerk befinden und die IP-Adresse des WebApp-Gerätes muss den Kits bekannt sein. 

**Aufteilung**

Da Severin Senn sich mit C# und vue.js mehr auf Seiten WebApp auskennt, hat er diese Aufgabe übernommen, wärend der Rest sich mit C++ und den IoT-Kits auseinandergesetzt hat. Jedoch haben wir uns auch untereinander Ausgeholfen und gemeinsam schwierigere Probleme behoben, sei es bei den Kits oder der WebApp.
