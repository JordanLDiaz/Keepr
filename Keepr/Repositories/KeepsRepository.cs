namespace Keepr.Repositories;

public class KeepsRepository
{
  private readonly IDbConnection _db;

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep Create(Keep keepData)
  {
    string sql = @"
    INSERT INTO keeps
    (creatorId, name, description, img)
    VALUES
    (@creatorId, @name, @description, @img);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = id;
    return keepData;
  }


  internal List<Keep> Get()
  {
    string sql = @"
    SELECT
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.CreatorId;
    ";
    List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }).ToList();
    return keeps;
  }

  internal Keep GetOne(int id)
  {
    string sql = @"
    SELECT 
    keeps.*,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    WHERE keeps.id = @id;
    ";
    Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { id }).FirstOrDefault();
    return keep;
  }
  internal bool EditKeep(Keep original)
  {
    string sql = @"
    UPDATE keeps
    SET 
    name = @name,
    description = @description,
    img = @img
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, original);
    return rows > 0;
  }

  internal string Remove(int id)
  {
    string sql = @"
    DELETE FROM keeps
    WHERE ID = @id;
    ";
    _db.Execute(sql, new { id });
    return "Keep was deleted.";
  }
}
