
namespace MFGameLib.Utils
{
    public class Configuarion
    {

        private int id;
        private string key;
        private string value;

        public Configuarion()
        {
        }

        public Configuarion(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public Configuarion(int id, string key, string value)
        {
            this.id = id;
            this.key = key;
            this.value = value;
        }

        public int getId()
        {
            return this.id;
        }

        public void setId(int id)
        {
            this.id = id;
        }

        public string getKey()
        {
            return key;
        }

        public void setKey(string key)
        {
            this.key = key;
        }

        public string getValue()
        {
            return value;
        }

        public void setValue(string value)
        {
            this.value = value;
        }
    }
}