namespace MusicWebApp.Models
{




    public class Tracks
    {
        public Item[] items { get; set; }
        public int limit { get; set; }
        public bool next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }

    public class Item
    {
        public DateTime added_at { get; set; }
        public Added_By added_by { get; set; }
        public bool is_local { get; set; }
        public object primary_color { get; set; }
        public Track track { get; set; }
    }

    public class Added_By
    {
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Track
    {
        public Album album { get; set; }
        public Artist1[] artists { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool episode { get; set; }
        public bool _explicit { get; set; }
        public string id { get; set; }
        public bool is_local { get; set; }
        public bool is_playable { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public bool track { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public Linked_From linked_from { get; set; }
    }

    public class Album
    {
        public string album_type { get; set; }
        public Artist[] artists { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public int total_tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

 

    public class Artist
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }




    public class Linked_From
    {
        public string id { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }



    public class Artist1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }






}
