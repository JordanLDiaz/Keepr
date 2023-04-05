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
    INSERT INTO JDkeeps
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
    JDkeeps.*,
    COUNT(JDvaultKeeps.id) AS kept,
    JDaccounts.*
    FROM JDkeeps
    JOIN JDaccounts ON JDaccounts.id = JDkeeps.CreatorId
    LEFT JOIN JDvaultKeeps ON JDvaultKeeps.keepId = JDkeeps.id
    GROUP BY JDkeeps.id;
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
    JDkeeps.*,
    COUNT(JDvaultKeeps.id) AS kept,
    JDaccounts.*
    FROM JDkeeps
    JOIN JDaccounts ON JDaccounts.id = JDkeeps.creatorId
    LEFT JOIN JDvaultKeeps ON JDvaultKeeps.keepId = JDkeeps.id
    WHERE JDkeeps.id = @id
    GROUP BY(JDkeeps.id);
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
    UPDATE JDkeeps
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
    DELETE FROM JDkeeps
    WHERE id = @id;
    ";
        int rows = _db.Execute(sql, new { id });
        return rows > 0;
    }
}