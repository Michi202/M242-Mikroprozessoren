# Send Temperature via Iot-Kit
**Autoren**
  - Noah Barth
  - Michael Marchesi

**Konzept**

Das sendTemperature-Programm basiert auf dem von der Lehrperson zur verfügung gestelltem http-Programm. Dabei liefert dies im Gegenzug zum Vorgänger anstelle eines String Temperaturwerte mittels HTTP-POST-request an eine vordefinierte Schnittstelle.
Die HTTP-Request wird an eine IP-Adresse im selben Netzwerk gestellt. Daher ist im momentanen Beispiel das Netzwerk "LERNKUBE" mit dem Passwort "l3rnk4b3" im mbed_config.h definiert. 

Die Temperatur wird am IoT-Kit mittels eines Sensors erkannt und in der Request als JSON-Objekt dem Empfänger zugestellt.

**Probleme**

Die grössten Herausforderungen stellten unter anderem der Syntax von C++ dar, sowie Flüchtigkeitsfehler auf eigener sowie auf Seiten der Schnittstelle
 
 **Anwendung**

Um das Programm auf einem IoT-Kit anzuwenden, dieses einfach an denn Rechner anschliessen. MBED-Studio erkennt das KIT automatisch und führt das Programm über dieses aus, sobald man das ganze Compiled.
