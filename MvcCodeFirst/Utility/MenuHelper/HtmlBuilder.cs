using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFirst.Utility.MenuHelper
{
    public class HtmlBuilder
    {
        //public void BuildTreeStruct(ViewModel.MenuViewModel<T> model)
        //{
        //    _CntNumber = 0;
        //    try
        //    {
        //        // 1.先设置放置根菜单的容器 
        //        _TopTagContainer = new TagContainer(ref _CntNumber, null);

        //        foreach (T mi in model.MenuItems)
        //        {
        //            BuildTagContainer(mi, _TopTagContainer);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //protected void BuildTagContainer(SysMenu item, TagContainer parent)
        //{
        //    TagContainer tc = FillTag(item, parent);

        //    foreach (SysMenu mmi in item.MenuChildren)
        //    {
        //        BuildTagContainer(mmi, tc);
        //    }
        //}
        //TagContainer FillTag(SysMenu item, TagContainer tc_parent)
        //{
        //    //先把本身的菜单项加上（每一个项都以li开始） 
        //    TagContainer li_tc = new TagContainer(ref _CntNumber, tc_parent);
        //    li_tc.Name = item.Name;
        //    li_tc.Tb = AddItem(item); //li tag 
        //    if (HasChildren(item))
        //    {
        //        TagContainer ui_container = new TagContainer(ref _CntNumber, li_tc);
        //        ui_container.Name = "**";
        //        ui_container.Tb = Add_UL_Tag();
        //        return ui_container;
        //    }
        //    return li_tc;
        //}
        //TagBuilder Add_UL_Tag()
        //{
        //    TagBuilder ul_tag = new TagBuilder("ul");
        //    ul_tag.AddCssClass("dropdown-menu");
        //    return ul_tag;
        //}

        //TagBuilder AddItem(SysMenu mi)
        //{
        //    var li_tag = new TagBuilder("li");
        //    var a_tag = new TagBuilder("a");
        //    var b_tag = new TagBuilder("b");
        //    var image_tag = new TagBuilder("img");

        //    if (mi.IconImage != null)
        //    {
        //        string path = "Images/" + mi.IconImage;
        //        image_tag.MergeAttribute("src", path);
        //    }

        //    b_tag.AddCssClass("caret");

        //    var contentUrl = GenerateContentUrlFromHttpContext(_htmlHelper);
        //    string a_href = GenerateUrlForMenuItem(mi, contentUrl);

        //    a_tag.Attributes.Add("href", a_href);

        //    if (mi.MenuType == MenuTypeOption.Top)
        //    {
        //        li_tag.AddCssClass("dropdown");
        //        a_tag.MergeAttribute("data-toggle", "dropdown");
        //        a_tag.AddCssClass("dropdown-toggle");
        //    }
        //    else
        //    {
        //        li_tag.AddCssClass("dropdown-submenu");
        //    }

        //    a_tag.InnerHtml += image_tag.ToString();
        //    a_tag.InnerHtml += mi.Name;

        //    if (HasChildren(mi))
        //    {
        //        a_tag.InnerHtml += b_tag.ToString();
        //    }

        //    li_tag.InnerHtml = a_tag.ToString();
        //    return li_tag;
        //}
    }
}