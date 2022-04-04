# Send Temperature via Iot-Kit
**Autoren**
  - Noah Barth
  - Michael Marchesi

**Konzept**

Das sendTemperature-Programm basiert auf dem von der Lehrperson zur Verfügung gestelltem HTTP-Programm. Dabei liefert dies im Gegenzug zum Vorgänger anstelle eines String Temperaturwerte mittels HTTP-POST Request an eine vordefinierte Schnittstelle.
Die HTTP-Request wird an eine IP-Adresse im selben Netzwerk gestellt. Daher ist im momentanen Beispiel das Netzwerk "LERNKUBE" mit dem Passwort "l3rnk4b3" im mbed_config.h definiert, das heisst falls die Geräte sich nicht im selben Netzwerk befinden kommt es zu Komplikationen.

Die Temperatur wird am IoT-Kit mittels eines Sensors erkannt und in der Request als JSON-Objekt dem Empfänger zugestellt.
Die übermittelten Temperaturen werden im Backend verglichen, falls der Unterschied beider Daten zu gross wäre, wird eine Fehlermeldung ausgegeben, dass der Temperaturunterschied zu hoch ist.

**Probleme**

Die grössten Herausforderungen stellten unter anderem der Syntax von C++ dar, da wir viele Strings übergeben mussten, sowie Flüchtigkeitsfehler auf eigener sowie auf Seiten der Schnittstelle.
 
 **Anwendung**

Um das Programm auf einem IoT-Kit anzuwenden, dieses einfach an denn Rechner anschliessen. MBED-Studio erkennt das KIT automatisch und führt das Programm über dieses aus.
