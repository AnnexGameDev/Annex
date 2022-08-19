namespace Annex_Old.Data.Serialization
{
    public interface IJsonSerializable
    {
        JsonElement GetJson();
        void SetFromJson(JsonElement jobject);
    }
}