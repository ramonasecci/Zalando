using System.Reflection.Metadata.Ecma335;
using Zalando.Models;
using static System.Net.WebRequestMethods;

namespace Zalando.Models
{
    public static class Db
    {
        private static int _maxId = 2;

        //crea lista articoli
        private static List<Articolo> _articoli = [
            new Articolo() { Id = 0, Name = "Stivale", Description = "Stivaletti stringati", ImgCover = "https://fenzy.it/media/uploads/public/product/18481-w1ax40602-02.jpg", Price = 45, Deleted = false, ImgDetails = ["https://m.media-amazon.com/images/I/816euPXLC0L._AC_SY695_.jpg", "https://m.media-amazon.com/images/I/91gdKP+IAGL._AC_SY695_.jpg"] },
            new Articolo() { Id = 1, Name = "Sneakers", Description = "Sneakers basse rosa", ImgCover = "https://m.media-amazon.com/images/I/71JlPUHpIkL._AC_SY500_.jpg", Price = 60, Deleted = false, ImgDetails = ["https://m.media-amazon.com/images/I/710oDOpsaqL._AC_SY500_.jpg", "https://m.media-amazon.com/images/I/71fZpPFOZAL._AC_SY500_.jpg"] },
            new Articolo() { Id = 2, Name = "Sandali", Description = "Sandali con plateau", ImgCover = "https://m.media-amazon.com/images/I/71Zrkcu5M6L._AC_SX500_.jpg", Price = 30, Deleted = false, ImgDetails = ["https://m.media-amazon.com/images/I/61F+69zZTzL._AC_SY695_.jpg", "https://m.media-amazon.com/images/I/71WcCm0EElL._AC_SX500_.jpg"] },
        ];

        //mostra tutti gli articoli della lista non eliminati
        public static List<Articolo> GetAll()
        {
            List<Articolo> articoli = [];
            foreach (var art in _articoli)
            {
                if (art.Deleted == false) articoli.Add(art);
            }
            return articoli;
        }

        //mostra tutti gli articoli della lista  eliminati
        public static List<Articolo> GetAllDeleted()
        {
            List<Articolo> artDeleted = [];
            foreach (var art in _articoli)
            {
                if (art.Deleted) artDeleted.Add(art);
            }
            return artDeleted;
        }


        //ripristina articolo da cestino a lista
        public static Articolo? Recover(int IdToRecover)
        {
            int? index = findArtIndex(IdToRecover);
            if (index != null)
            {

                var artRecovered = _articoli[(int)index];
                artRecovered.Deleted = false;
                return artRecovered;
            }

            return null;

        }

        //trova articolo nella lista
        private static int? findArtIndex(int id)
        {
            int i;
            bool artFound = false;
            for (i = 0; i < _articoli.Count; i++)
            {
                if (_articoli[i].Id == id)
                {
                    artFound = true;
                    break;
                }
            }

            if (artFound) return i;
            return null;
        }


        //ritorna un articolo in base all'ID
        public static Articolo? GetById(int? id)
        {
            if (id == null) return null;
            for (int i = 0; i < _articoli.Count; i++)
            {
                Articolo art = _articoli[i];
                if (art.Id == id) return art;
            }
            return null;
        }

        //aggiunge elemento alla lista
        public static Articolo Add(Articolo articolo)
        {
            _maxId++;
            articolo.Id = _maxId;
            articolo.Deleted = false;
            _articoli.Add(articolo);
            return articolo;
        }


        //modifica articolo
        public static Articolo? Edit(Articolo articolo)
        {
            int? index = findArtIndex(articolo.Id);
            if (index != null)
            {
                _articoli[(int)index].Name = articolo.Name;
                _articoli[(int)index].ImgCover = articolo.ImgCover;
                _articoli[(int)index].Price = articolo.Price;
                _articoli[(int)index].Description = articolo.Description;
                _articoli[(int)index].ImgDetails[0] = articolo.ImgDetails[0];
                _articoli[(int)index].ImgDetails[1] = articolo.ImgDetails[1];
                return _articoli[(int)index];
            }
            return null;
        }



        //aggiunge elemento al cestino
        public static Articolo? SoftDelete(int idToDelete)
        {
            int? deletedI = findArtIndex(idToDelete);
            if (deletedI != null)
            {
                var artDeleted = _articoli[(int)deletedI];
                artDeleted.Deleted = true;
                return artDeleted;
            }
            return null;
        }


        //elimina definitivamente 
        public static Articolo? HardDelete(int idToDelete)
        {
            int? deletedI = findArtIndex(idToDelete);
            if (deletedI != null)
            {
                var artDeleted = _articoli[(int)deletedI];
                _articoli.RemoveAt((int)deletedI);
                return artDeleted;
            }
            return null;
        }




    }
}
