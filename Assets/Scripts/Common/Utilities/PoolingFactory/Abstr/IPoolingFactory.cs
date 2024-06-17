namespace FpvDroneSimulator.Common.Utilities.PoolingFactory.Abstr
{
    public interface IPoolingFactory<TBase, in TCreationOptions>
    {
        public TBase Create(TCreationOptions options);
        public void Delete(TBase unit);
    }
}