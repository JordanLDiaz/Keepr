namespace Keepr.Services;

public class KeepsService
{
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }

  internal Keep Create(Keep keepData)
  {
    Keep keep = _repo.Create(keepData);
    return keep;
  }

  internal List<Keep> GetAll()
  {
    List<Keep> keeps = _repo.GetAll();
    return keeps;
  }

  internal Keep GetOne(int id, string userId)
  {
    Keep keep = _repo.GetOne(id);
    if (keep.Creator == null)
    {
      throw new Exception("No keep at this id.");
    }

    if (keep.CreatorId != userId)
    {
      keep.Views++;
      _repo.EditKeep(keep);
    }
    return keep;
  }

  internal Keep EditKeep(Keep updateData, int id, string userId)
  {
    Keep original = GetOne(updateData.Id, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("This is not your keep to edit.");
    }
    original.Name = updateData.Name ?? original.Name;
    original.Description = updateData.Description ?? original.Description;
    original.Img = updateData.Img ?? original.Img;

    _repo.EditKeep(original);
    return original;
  }

  internal string Remove(int id, string userId)
  {
    Keep keep = GetOne(id, userId);
    if (keep == null)
    {
      throw new Exception("No keep exists at this id.");
    }
    if (keep.CreatorId != userId)
    {
      throw new Exception("You do not have permission to delete this keep.");
    }
    _repo.Remove(id);
    return $"{keep.Name} was removed.";
  }
}
