#include <stdio.h>
#include <string.h>
#include <windows.h>
#include <unistd.h>


int main ()
{
  char IP_server[] = "shaypc";
  char remoteAppName[] = "notepad++";
  char fileName[] = "remoteApp.rdp";
  FILE * fp;


  fp = fopen (fileName,"w");


  fprintf (fp, "allow desktop composition:i:1%c", 10);
  fprintf (fp, "allow font smoothing: i:1%c", 10);
  fprintf (fp, "alternate full address: s:%s%c", IP_server, 10);
  fprintf (fp, "alternate shell: s:rdpinit.exe%c", 10);
  fprintf (fp, "devicestoredirect: s:*%c", 10);
  fprintf (fp, "full address: s:%s%c", IP_server, 10);
  fprintf (fp, "prompt for credentials on client:i:1%c", 10);
  fprintf (fp, "promptcredentialonce: i:0%c", 10);
  fprintf (fp, "redirectcomports: i:1%c", 10);
  fprintf (fp, "redirectdrives: i:1%c", 10);
  fprintf (fp, "remoteapplicationmode: i:1%c", 10);
  fprintf (fp, "RemoteProgram: s:%s%c", remoteAppName, 10);
  fprintf (fp, "remoteapplicationprogram: s:||%s%c", remoteAppName, 10);
  fprintf (fp, "span monitors: i:1%c", 10);
  fprintf (fp, "use multimon: i:1%c", 10);


  fclose (fp);


  system(fileName);
  Sleep(10);
  do{remove(fileName);}while(access( fileName, F_OK ) == 0);


  return 0;
}

