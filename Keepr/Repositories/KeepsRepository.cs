namespace Keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    public Keep Create(Keep keepData)
    {
        string sql = @"
    INSERT INTO keeps
    (creatorId, name, description, img, views)
    VALUES
    (@creatorId, @name, @description, @img, @views);
    SELECT LAST_INSERT_ID();
    ";
        int id = _db.ExecuteScalar<int>(sql, keepData);
        keepData.Id = id;
        return keepData;
    }

    public List<Keep> GetAll()
    {
        string sql = @"
    SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.CreatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    GROUP BY keeps.id;
    ";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }).ToList();
        return keeps;
    }

    public Keep GetOne(int id)
    {
        string sql = @"
    SELECT 
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
    FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    WHERE keeps.id = @id
    GROUP BY(keeps.id);
    ";
        return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
        {
            keep.Creator = profile;
            return keep;
        }, new { id }).FirstOrDefault();
    }

    public bool EditKeep(Keep original)
    {
        string sql = @"
    UPDATE keeps
    SET 
    name = @name,
    description = @description,
    img = @img,
    views = @views
    WHERE id = @id;
    ";
        int rows = _db.Execute(sql, original);
        return rows > 0;
    }

    public bool Remove(int id)
    {
        string sql = @"
    DELETE FROM keeps
    WHERE id = @id;
    ";
        int rows = _db.Execute(sql, new { id });
        return rows > 0;
    }
}