# Send Temperature via Iot-Kit
**Autoren**
  - Alessandro Pisani

**Konzept**

Das SendNFC-Programm basiert auf dem von der Lehrperson zu Verfügung gestelltem HTTP-Programm.
Wenn ein neues NFC Karte/Batch erkannt wird, wird diese mittels HTTP_Request an eine vordefinierte Schnittstelle gesendet.

Die UID wird am IoT-Kit mittels eines Sensors erkannt und in der Request als JSON-Objekt dem Empfänger zugestellt.
Die übermittelte UID wir im Backend verglichen, falls ein User existiert, und eine Berechtigung hat, wird der Zugriff erlaubt, ansonsten wird der Zugriff verweigert.

**Probleme**

Meine Herausforderung war grösstens, das knowhow am der Sprache. C++ war neuland für mich. Ich habe versucht mich findig zu machen unm zu verstehen, wie dies ungefär funktoniert. Mit hilfe der verschiedenen Repos auf git, welche wir von unserem Lehrer zu verfügung gestellt bekommen haben, konnte ich mich gut orientiren. Danach ging alles soweit gut ab.
 
 **Anwendung**

Um das Programm auf einem IoT-Kit anzuwenden, dieses einfach an denn Rechner anschliessen. MBED-Studio erkennt das KIT automatisch und führt das Programm über dieses aus.
Anschliessend muss man eine NFC-Karte an unseren Sensonr halten, damitt es los gehen kann!
