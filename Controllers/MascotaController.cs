using Newtonsoft.Json;
using PruebaTecnica.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PruebaTecnica.Front.Controllers
{
    public class MascotaController : Controller
    {
        
        string url = "https://localhost:44307/api/Mascotas";

        //Lista de las mascotas en la veterinaria
        public async Task<ActionResult> IndexAsync()
        {
            var httpcliente = new HttpClient();
            var json = await httpcliente.GetStringAsync(url);
            List<Mascota> Mascotaslista = JsonConvert.DeserializeObject<List<Mascota>>(json);
            return View(Mascotaslista);
        }



        // POST: Codigo de agregar mascota
       
        public async Task<ActionResult> Create(Mascota b)
        {
            using(var httpcliente = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(b), Encoding.UTF8, "application/json");
                HttpResponseMessage respond = await httpcliente.PostAsync(url, content);
                if (respond.IsSuccessStatusCode)
                {
                    return Redirect("~/Mascota/IndexAsync");
                }
                
            }
            return View();
        }


        // GET: Codigo de mostrar por json el usuario que se va a editar por el id
       [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var httpcliente = new HttpClient();
            var json = await httpcliente.GetStringAsync("https://localhost:44307/api/Mascotas/"+id);
            var mascota = JsonConvert.DeserializeObject<Mascota>(json);
            return View(mascota);

        }

        // POST: Codigo de Editar el perfil de la mascota
        public async Task <ActionResult> Edit(int id, Mascota a)
        {
            using (var httpcliente = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(a), Encoding.UTF8, "application/json");
                using (var EditarMascota = await httpcliente.PutAsync("https://localhost:44307/api/Mascotas/" + id, content))
                {
                    String Respond = await EditarMascota.Content.ReadAsStringAsync();
                    List<Mascota> mascotalista = JsonConvert.DeserializeObject<List<Mascota>>(Respond);

                    HttpResponseMessage mensaje = (EditarMascota);

                    if (mensaje.IsSuccessStatusCode)
                    {
                        return Redirect("~/Mascota/IndexAsync");
                    }
                }

            }

            return View();
        }

        // POST: Codigo de eliminar Perfil de la mascota
        
        public async Task<ActionResult> Delete(int id)
        {
            var httpcliente = new HttpClient();
            var json = await httpcliente.DeleteAsync("https://localhost:44307/api/Mascotas/" + id.ToString());
            HttpResponseMessage respuesta = (json);

            if (respuesta.IsSuccessStatusCode)
            {
                return Redirect("~/Mascota/IndexAsync");
            }

            return View();
        }
        
    }
}
