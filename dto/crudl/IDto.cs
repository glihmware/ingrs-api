
namespace Ingrs.Dto
{

  public interface IDto<T>
  {
    public abstract void FromEntity(T e);
    public abstract T ToEntity();

  }

}
