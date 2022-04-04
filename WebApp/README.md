# M242-Mikroprozessoren WebApp
Dieses Abteil ist das Herz stück unseres Programmes. Es Komunizirt mit den Ionitikits und schaut, dass das Login sicher und richtig abläuft. Es ist eine WebApplikation die in 2 Teile gespalten ist (Frontend,Backend).

**Autor**  
Severin Senn

## Lernjournal
Ich habe durch diese Projekt sehr viel gelernt übe die Komunikation mit mehren Geräten. Ich konnte durch diese Projekt wachsen und neus Entdecken. Natürlich gab es auch Probleme das wohl grösste war die Komunikation zwischen den Geräten mittels Rest Protokoll, da alles sehr Asynchron verläuft musste ich hin und wieder mal mittels Logik und Timespans Tricksens. Alles in allem hat es mir aber sehr Spass gemacht und mich gefreut das ich so etwas mal machen durfte.  

## Frontend Installation
Das frontent kann mittels [NodeJs](https://nodejs.org) zum laufen gebracht werden. Dafür Navigiren sie in den Ordner "M242.Web". Wenn sie in diesem Verzeichniss sind lassen sie den Command 
```bash
npm install
```
laufen um die dependencies zu instaliren. Wenn sie dies gemacht haben müssen sie noch in der Config Datei sse.env.js anpassen die im Verzeichnis Config liegt. In diesem File müssen sie den Key API_URL anpassen. Dieser sollte ihr Url zeigen auf dem das Backend läuft. Wichtig: am Ende der Url Sollte /api stehen. Wenn sie dies alles gemcht haben sollten sie auf das Root Verzeichnis "M242.Web" navigiren und den Command
```bash
npm run dev-sse
```
starten können. Den Rest wird wird für sie gemacht. In ihrem Cmd sollte dann am ende eine Localhost Url mit einem Port stehen sie müssen nur noch einen Web Browser öffnen und diese Url eingeben.
## Backend
Das backend besteht aus einem Standart Visual studio Api mit einem NHilbernate Orm für die Datenbank.
### Konfiguration
Um die App zu Konfiguriren müssen sie die Datei M242.Api/appsettings.json anpassen.
Dort gibt es die Section ConnectionStrings wo sie ihre Sql Connection String Anpassen wie auch die Section WebPageBlockPoint. Diese WebPageBlockPoint bestimmt darüber ab wie viel Grad die Webseite gesperrt wird. 

!! Die Datenbank die sie dazu benötigen ist PostgresSql
### Starten
Da dies eine eine Visual studio App ist müssen sie diese Einfach mit Visual Studio öffnen und auf start Klicken. Meine Empfelung ist es diese App über den IIs laufen zu lassen. Um die Webseite gelich über Port 80 laufen zu lassen.

!! Wenn sie es nicht Hinbekommen gibt es die Möglichkeit mich auf Teams zu kontaktiren dann kann ich ihnen eine exe Kompliren lassen dies sie dann einfach starten können.
Teams: Severin.Senn1
