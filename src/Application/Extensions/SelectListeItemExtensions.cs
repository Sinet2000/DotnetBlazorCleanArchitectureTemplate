using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAAgentura.Application.Extensions;

public static class SelectListItemExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItemWithGroup<T>(this IEnumerable<T> items,
            Func<T, string> textSelector, Func<T, long> valueSelector, string group)
        {
            var list = items.OrderBy(item => textSelector(item)).Select(item =>
                new SelectListItem
                {
                    Selected = false,
                    Text = textSelector(item),
                    Value = valueSelector(item),
                    Group = group,
                }).ToList();

            return list;
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector)
        {
            return ToSelectListItems(items, textSelector, valueSelector, false);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, bool allowEmpty)
        {
            return ToSelectListItems(items, textSelector, valueSelector, allowEmpty, i => false);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, Func<T, bool> selected)
        {
            return ToSelectListItems(items, textSelector, valueSelector, false, selected);
        }

        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, bool allowEmpty, Func<T, bool> selected)
        {
            var list = items.OrderBy(item => textSelector(item)).Select(item =>
                new SelectListItem
                {
                    Selected = selected(item),
                    Text = textSelector(item),
                    Value = (long)valueSelector(item)
                }).ToList();

            if (allowEmpty)
                list.InsertEmptyOption();

            return list;
        }

        public static IEnumerable<SelectListItemString> ToSelectListStringItems<T>(
            this IEnumerable<T> items,
            Func<T, string> textSelector,
            Func<T, string> valueSelector,
            Func<T, IEnumerable<SelectListItemString>>? groupItemsSelector = null)
        {
            var list = items.OrderBy(textSelector).Select(item =>
                new SelectListItemString(textSelector(item), valueSelector(item), groupItemsSelector?.Invoke(item))
                ).ToList();

            return list;
        }

        public static IEnumerable<SelectListItemString> GetEmptyList()
        {
            return new List<SelectListItemString>()
            {
                new SelectListItemString("", "")
            };
        }

        private static List<SelectListItem> InsertEmptyOption(this List<SelectListItem> items)
        {
            items.Insert(0, new SelectListItem
            {
                Selected = false,
                Text = "",
                Value = 0
            });

            return items;
        }

        public static IEnumerable<ExtendedSelectListItem> ToExtendedSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, Func<T, object> additionalDataSelector)
        {
            return ToExtendedSelectListItems(items, textSelector, valueSelector, additionalDataSelector, false);
        }

        public static IEnumerable<ExtendedSelectListItem> ToExtendedSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, Func<T, object> additionalDataSelector, bool allowEmpty)
        {
            return ToExtendedSelectListItems(items, textSelector, valueSelector, additionalDataSelector, allowEmpty, i => false);
        }

        public static IEnumerable<ExtendedSelectListItem> ToExtendedSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, Func<T, object> additionalDataSelector, Func<T, bool> selected)
        {
            return ToExtendedSelectListItems(items, textSelector, valueSelector, additionalDataSelector, false, selected);
        }

        public static IEnumerable<ExtendedSelectListItem> ToExtendedSelectListItems<T>(this IEnumerable<T> items, Func<T, string> textSelector, Func<T, object> valueSelector, Func<T, object> additionalDataSelector, bool allowEmpty, Func<T, bool> selected)
        {
            var list = items.OrderBy(item => textSelector(item)).Select(item =>
                new ExtendedSelectListItem
                {
                    Selected = selected(item),
                    Text = textSelector(item),
                    Value = (long)valueSelector(item),
                    AdditionalData = additionalDataSelector(item).ToString()
                }).ToList();

            if (allowEmpty)
                list.InsertEmptyOption();

            return list;
        }

        private static List<ExtendedSelectListItem> InsertEmptyOption(this List<ExtendedSelectListItem> items)
        {
            items.Insert(0, new ExtendedSelectListItem
            {
                Selected = false,
                Text = "",
                Value = 0,
                AdditionalData = null
            });

            return items;
        }
    }

    public class SelectListItem
    {
        public SelectListItem() { }

        public SelectListItem(string text, long value)
            : this()
        {
            Text = text;
            Value = value;
        }

        public SelectListItem(string text, long value, bool selected)
            : this(text, value)
        {
            Selected = selected;
        }

        public SelectListItem(string text, long value, bool selected, bool disabled)
            : this(text, value, selected)
        {
            Disabled = disabled;
        }

        public SelectListItem(string text, long value, string group)
            : this(text, value)
        {
            Group = group;
        }
        
        public SelectListItem(string text, long value, SelectListItem groupItem)
            : this(text, value)
        {
            GroupItem = groupItem;
            Group = groupItem.Text;
        }

        public bool Disabled { get; set; }
        public string Group { get; set; }
        
        public SelectListItem GroupItem { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public long Value { get; set; }

        public string GetGroupNameFromGroupItem() => GroupItem.Text;
    }

    public class SelectListItemString
    {
        public SelectListItemString() { }

        public SelectListItemString(string text, string value)
            : this()
        {
            Text = text;
            Value = value;
        }

        public SelectListItemString(string text, string value, IEnumerable<SelectListItemString>? groupItems)
            : this(text, value)
        {
            GroupItems = groupItems;
        }

        public string Text { get; set; }
        public string Value { get; set; }

        public IEnumerable<SelectListItemString>? GroupItems { get; set; }
    }

    public class ExtendedSelectListItem : SelectListItem
    {
        public object? AdditionalData { get; set; }
    }

    public class SelectListGroup
    {
        public bool Disabled { get; set; }

        public string Name { get; set; }

        public SelectListGroup()
        {

        }

        public SelectListGroup(string name)
        {
            Name = name;
        }
    }