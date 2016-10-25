using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class PosteHelper
    {
        public async static Task<List<postes>> SelectAllByNumeroLocalAsync(string numLocal)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.postes
                            orderby p.numeroPoste ascending
                            join l in ctx.locaux on p.idLocal equals l.idLocal
                            where l.numero == numLocal
                            select p;
                return await query.ToListAsync();
            }
        }

        public async static Task<etatspostes> SelectEtatAsync(int idPoste)
        {
            using (var ctx = new Connexion420())
            {
                var query = from p in ctx.postes
                            orderby p.numeroPoste ascending
                            join e in ctx.etatspostes on p.idEtatPoste equals e.idEtatPoste
                            where p.idPoste == idPoste
                            select e;
                etatspostes obj = await query.SingleOrDefaultAsync();
                return obj;
            }
        }

    }
}
