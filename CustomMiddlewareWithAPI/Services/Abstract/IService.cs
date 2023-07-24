namespace CustomMiddlewareWithAPI.Services.Abstract;

public interface IService<T>
{
	IEnumerable<T> GetAll();
	T Get(int  id);
	void Add (T entity);
	void Update (T entity);
	void Delete (int id	);

}
