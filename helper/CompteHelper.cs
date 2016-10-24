using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamaBD.helper
{
    public class CompteHelper
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

        public async static Task<comptes> SelectCompte(string nomUtilisateur)
        {
            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            where c.nomUtilisateur == nomUtilisateur
                            select c;
                comptes obj = await query.SingleOrDefaultAsync();
                return obj;
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

        /// <summary>
        /// Retourne de façon async une liste de compte selon le critère de sélection s'il est administrateur ou non.
        /// Par défaut retourne tout les administrateur.
        /// </summary>
        /// <param name="estAdmin">Vrai par défaut, mettez à false pour retourner les non admins.</param>
        /// <returns></returns>
        public static async Task<List<comptes>> SelectAllAdminAsync(bool estAdmin = true)
        {
            using (var ctx = new Connexion420())
            {
                var query = from c in ctx.comptes
                            where c.estAdmin == estAdmin
                            orderby c.nomUtilisateur ascending
                            select c;
                return await query.ToListAsync();
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
                courant.courriel = obj.courriel;
                courant.estAdmin = obj.estAdmin;
                courant.matricule = obj.matricule;
                courant.motDePasse = obj.motDePasse;
                courant.nom = obj.nom;
                //courant.nomUtilisateur = courant.nomUtilisateur;
                courant.prenom = obj.prenom;
                await ctx.SaveChangesAsync();
                return true;
            }
            //Erreur possible
            return false;
        }
    }
}
