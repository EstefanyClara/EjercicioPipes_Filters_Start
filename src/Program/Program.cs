using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU; 

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            PictureProvider provider = new PictureProvider(); 
            IPicture picture= provider.GetPicture(@"C:\Users\estef\OneDrive - Universidad Católica del Uruguay\Documentos\UCU\SEGUNDO SEMESTRE\PROGRAMACION 2\Pipes_filters_start\EjercicioPipes_Filters_Start\src\Program\beer.jpg"); 
            
            //Creo primero los pipes del ultimo al primero 
            
            IFilter filtroGris= new FilterGreyscale();
            IFilter filtroNegativo= new FilterNegative();
            IFilter filtroAzul= new FilterBlurConvolution(); 

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

            
        
        
        }
    }
}
