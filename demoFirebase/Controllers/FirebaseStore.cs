using Firebase.Auth;
using Firebase.Storage;

namespace demoFirebase.Controllers
{
    public static class FirebaseStore
    {
        #region Subir archivos al firebase Storage
        public static async Task<string> SubirStorage(Stream archivo, string nombre)
        {
            try
            {
                string email = "estefleomar@gmail.com";
                string clave = "Polaris123*";
                string ruta = "demofirebase-6ce30.appspot.com";
                string api_key = "AIzaSyDQTc_MzFYcSNQetZOzvdgm8XyqkJS4Ih0";

                var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
                var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);

                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage
                    (
                        ruta,
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                            ThrowOnCancel = true,
                        })
                        .Child("Fotos_Perfil")
                        .Child(nombre)
                        .PutAsync(archivo, cancellation.Token);

                var donwloadURL = await task;
                return donwloadURL;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion




        #region Eliminar archivos al firebase Storage
        public static async Task EliminarStorage(string nombreArchivo)
        {
            try
            {
                string email = "estefleomar@gmail.com";
                string clave = "Polaris123*";
                string ruta = "demofirebase-6ce30.appspot.com";
                string api_key = "AIzaSyDQTc_MzFYcSNQetZOzvdgm8XyqkJS4Ih0";

                var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
                var a = await auth.SignInWithEmailAndPasswordAsync(email, clave);
                var cancellation = new CancellationTokenSource();

                await new FirebaseStorage(
                    ruta,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true,
                    })
                    .Child("Fotos_Perfil")
                    .Child(nombreArchivo)
                    .DeleteAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}
