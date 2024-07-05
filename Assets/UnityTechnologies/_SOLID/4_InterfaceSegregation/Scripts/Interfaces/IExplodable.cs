
namespace DesignPatterns.ISP
{
    /// <summary>
    /// Defines a contract for objects that can explode.
    /// </summary>
    public interface IExplodable
    {
        // Triggers an explosion (e.g. particles or other GameObject effects)
        void Explode();
    }

}
