using System.Collections.Generic;
using System.Linq;

namespace Rutracker.Anime.Models
{
    public class Anime
    {
        public Anime() {
            OtherInfo = new List<string>();
            Video = new List<VideoContent>();
        }

        public string Title { get; set; }
        public IEnumerable<string> Names { get; set; }
        public IList<VideoContent> Video { get; set; }
        public Traits Traits { get; set; }
        public IEnumerable<Type> Types { get; set; }
        public IList<string> OtherInfo { get; set; }

        #region equality members

        protected bool Equals(Anime other) {
            return Equals(Names, other.Names) && Video.SequenceEqual(other.Video) && Equals(Traits, other.Traits) && 
                Types.SequenceEqual(other.Types) && OtherInfo.SequenceEqual(other.OtherInfo);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Anime) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (Names != null ? Names.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Video != null ? Video.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Traits != null ? Traits.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Types != null ? Types.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (OtherInfo != null ? OtherInfo.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

        public override string ToString() {
            return string.Format("Names: {0}, Video: {1}, Traits: {2}, Types: {3}, OtherInfo: {4}",
                Names.SequenceToString(), Video.SequenceToString(), Traits, Types.SequenceToString(), OtherInfo.SequenceToString());
        }

        public enum Type
        {
            TV = 1,
            Movie,
            OVA,
            ONA,
            Special
        }
    }

    public class VideoContent
    {
        public Series Series { get; set; }
        public IEnumerable<string> AudioAndSubs { get; set; }

        #region equality members

        protected bool Equals(VideoContent other) {
            return Equals(Series, other.Series) && AudioAndSubs.SequenceEqual(other.AudioAndSubs);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((VideoContent) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((Series != null ? Series.GetHashCode() : 0)*397) ^ (AudioAndSubs != null ? AudioAndSubs.GetHashCode() : 0);
            }
        }

        #endregion

        public override string ToString() {
            return string.Format("Series: {0}, AudioAndSubs: {1}", Series, "[" + string.Join(", ", AudioAndSubs) + "]");
        }
    }
}