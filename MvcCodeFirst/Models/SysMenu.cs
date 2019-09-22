﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcCodeFirst.Models
{
    public class SysMenu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("MenuID")]
        public int ID { get; set; }
        public int? ParentID { get; set; }
        [DisplayName("名称")]
        [StringLength(50)]
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        [DisplayName("图标")]
        public string IconImage { get; set; }
        public MenuTypeOption MenuType { get; set; }
        public List<SysMenu> MenuChildren = new List<SysMenu>();
        [DisplayName("描述")]
        public string Description { get; set; }
    }

    public enum MenuTypeOption
    {
        Submenu = 0,
        Top = 1
    }
}