using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU; 
using CognitiveCoreUCU; 

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider(); 
            IPicture picture= provider.GetPicture(@"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\luke.jpg"); 
            
            //Creo primero los pipes del ultimo al primero 
            
            IFilter filtroGris= new FilterGreyscale();
            IFilter filtroNegativo= new FilterNegative();
            IFilter filtroAzul= new FilterBlurConvolution(); 
            
            //
            PipeNull pipeNull= new PipeNull(); 
            PipeSerial pipeTwo= new PipeSerial(filtroNegativo, pipeNull); 
            picture= pipeTwo.Send(picture);  
            provider.SavePicture(picture, @"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\beer1.jpg");
            
            var twitter= new TwitterImage(); 
            Console.WriteLine(twitter.PublishToTwitter("ProbandoEC", @"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\beer1.jpg")); 
            
            PipeSerial pipeOne= new PipeSerial(filtroGris, pipeTwo);
            picture= pipeOne.Send(picture);            
            provider.SavePicture(picture, @"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\beer2.jpg"); 
            Console.WriteLine(twitter.PublishToTwitter("Probando2EC", @"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\beer2.jpg")); 
            
            PipeNull pipeNull= new PipeNull();  
            //PipeFork pipeConditionalFork= new PipeFork(pipeTieneCara, pipeNoCara); 
     
    
            CognitiveFace face = new CognitiveFace();  
            face.Recognize(@"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\luke.jpg"); 
           

            if (face.FaceFound)
            {
                PipeSerial pipeTieneCara = new PipeSerial(filtroGris, pipeNull);
                picture= pipeTieneCara.Send(picture); 
                provider.SavePicture(picture, @"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\lukecara.jpg"); 
                Console.WriteLine(twitter.PublishToTwitter("Se identificó una cara", @"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\lukecara.jpg"));
            }
            else
            {
                PipeSerial pipeNoCara= new PipeSerial (filtroNegativo, pipeNull);
                picture= pipeNoCara.Send(picture); 
                provider.SavePicture(picture, @"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\nocara.jpg");
                Console.WriteLine(twitter.PublishToTwitter("No se identifica cara", @"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\nocara.jpg"));
           

            }
            
        }

        
    }
}
