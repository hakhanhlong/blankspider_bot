using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankSpider.Api.Entities
{
    public class Source
    {
        public Source() { }


        public string created_by { get; set; }
        public DateTime created_date { get; set; }
        public string id { get; set; }

        public bool is_active { get; set; }

        public string mode { get; set; }

        public string name { get; set; }
        public string project_id { get; set; }

        public string project_name { get; set; }

        public string status { get; set; }
        public string type_spider { get; set; }

        public string server_ip { get; set; }



    }

    public class SourceConfigGeneral
    {
        public SourceConfigGeneral() { }

        public string base_url { get; set; }
        public string max_trying_count { get; set; }
        public string thread_number { get; set; }
        public string thread_sleep { get; set; }

        public string post_url { get; set; }

        public string thread_number_parsing { get; set; }

        public string update_sleep { get; set; }

        public string video_base_url { get; set; }

        public string chk_unique_css { get; set; }
        public string filter_pdf { get; set; }

        public string remove_filter_pdf { get; set; }

        public string video_post_url { get; set; }


    }


    public class SourceConfigVideo
    {
        public SourceConfigVideo() { }

        public List<ConfigVideo> configvideos { get; set; }

    }

    public class ConfigVideo
    {
        public string name { get; set; }

        public ConcreteVideoField config { get; set; }

    }

    public class ConcreteVideoField
    {
        public string pattern_type { get; set; }
        public Dictionary<string, ItemParseVideoField> step { get; set; }
    }

    public class ItemParseVideoField
    {
        public string field_value { get; set; }
        public string is_url_video_cache { get; set; }



    }


    public class SourceConfigLink
    {
        public SourceConfigLink() { }

        public List<ConfigLink> configlinks { get; set; }

    }

    public class ConfigLink
    {
        public ConfigLink() { }

        public string link_type { get; set; }
        public string pattern_type { get; set; }
        public string url_pattern { get; set; }

    }

    public class ConcreteLink
    {
        public ConcreteLink() { }

        public string link_type { get; set; }
        public string href { get; set; }
    }


    public class SourceConfigField
    {
        public SourceConfigField() { }

        public List<ConfigField> configfields { get; set; }
    }

    public class ConfigField
    {
        public string name { get; set; }

        public ConcreteField config { get; set; }

    }
   

    public class ConcreteField
    {
        public string pattern_type { get; set; }
        public Dictionary<string, ItemPerseField> step { get; set; }
    }


    public class ItemPerseField
    {
        public string end_pattern { get; set; }
        public string start_pattern { get; set; }

        public string remove_html { get; set; }

        public string break_parsing { get; set; }

        public string xpath { get; set; }
    }
}
