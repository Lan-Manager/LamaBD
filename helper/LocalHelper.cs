using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class LocalHelper
    {
        public async static Task<List<locaux>> SelectAllAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from l in ctx.locaux
                            orderby l.numero ascending
                            select l;
                return await query.ToListAsync();
            }
        }

        public static async Task<locaux> SelectByNumeroAsync(string numero)
        {
            using (var ctx = new Connexion420())
            {
                var query = from l in ctx.locaux
                            where l.numero == numero
                            select l;
                locaux obj = await query.SingleOrDefaultAsync();
                return obj;
            }
        }


        public async static Task<List<locaux>> SelectLocauxTournoiAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from l in ctx.locaux
                            orderby l.numero ascending
                            join tl in ctx.tournoislocaux on l.idLocal equals tl.idLocal
                            join t in ctx.tournois on tl.idTournoi equals t.idTournoi
                            where t.enCours == true
                            select l;
                return await query.ToListAsync();
            }
        }
    }
}
