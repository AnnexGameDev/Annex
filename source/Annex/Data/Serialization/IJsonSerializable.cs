namespace Annex.Data.Serialization
{
    public interface IJsonSerializable
    {
        JsonElement GetJson();
        void SetFromJson(JsonElement jobject);
    }
}