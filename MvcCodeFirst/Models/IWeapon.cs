using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFirst.Models
{
    public interface IWeapon
    {
        string Hit(string target);
    }

    public class Sword : IWeapon
    {
        public string Hit(string target)
        {
            return string.Format("Chopped {0} clean in half", target);
        }
    }

    public class Shuriken : IWeapon
    {
        public string Hit(string target)
        {
            return string.Format("Pierced {0}'s armor", target);
        }
    }
    public class Samurai
    {
        readonly IWeapon weapon;
        public Samurai(IWeapon weapon)
        {
            this.weapon = weapon;
        }

        public string Attack(string target)
        {
            return this.weapon.Hit(target);
        }
    }


}