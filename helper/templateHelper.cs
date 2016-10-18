using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    class templateHelper
    {
        public async static Task<List<comptes>> SelectAllAsync()
        {
            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            orderby c.nomUtilisateur ascending
                            select c;
                return await query.ToListAsync();
            }
        }

        public static async Task<comptes> SelectByIDAsync(string id)
        {
            int num;

            bool success = int.TryParse(id, out num);
            if (!success)
                return null;

            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            where c.idCompte == num
                            select c;
                comptes obj = await query.SingleOrDefaultAsync();
                return obj;
            }
        }

        public static async Task<bool> InsertAsync(comptes obj)
        {
            using (var ctx = new Connexion420())
            {
                ctx.comptes.Add(obj);
                await ctx.SaveChangesAsync();
                return true;
            }
            //Erreur possible
            return false;
        }

        public static async Task<bool> UpdateAsync(comptes obj)
        {
            using (var ctx = new Connexion420())
            {
                comptes courant = await ctx.comptes.FindAsync(obj.idCompte);
                //courant.nomUtilisateur = courant.nomUtilisateur;
                await ctx.SaveChangesAsync();
                return true;
            }
            //Erreur possible
            return false;
        }
    }
}
