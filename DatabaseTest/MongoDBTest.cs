using Xunit;
using Bogus;
using Database;
using DatabaseTest.TestEntities;
using DatabaseTest.Fixtures;
using System;
using Xunit.Abstractions;

namespace DatabaseTest;

public class MongoDBTest : IClassFixture<MongoDBFixture>
{
    private readonly MongoDBFixture fixture;
    private MongoDBTestEntity entity;
    private Faker faker;
    private ITestOutputHelper logger;
    public MongoDBTest(ITestOutputHelper logger ,MongoDBFixture fixture)
    {
        this.fixture = fixture;
        this.faker = new Faker();
        this.entity = new MongoDBTestEntity()
        {
            Name = faker.Name.FirstName(),
            Text = faker.Rant.Review()
        };
        this.logger = logger;
    }


    [Fact]
    public void IHopeToInsert()
    {
        Exception exception = null;
        try{
            this.fixture.dao.Insert(this.entity);
        }
        catch(Exception e){
            exception = e;
        }

        Assert.Null(exception);
    }

    [Fact]
    public void IHopeToUpdate()
    {
        Exception exception = null;
        this.fixture.dao.Insert(this.entity);
        this.entity.Text = faker.Rant.Review();

        try
        {
            this.fixture.dao.Update(entity);
        } catch(Exception e)
        {
            exception = e;
        }

        Assert.Null(exception);
    }

    [Fact]
    public void IHopeToDelete()
    {
        Exception exception = null;
        this.fixture.dao.Insert(this.entity);
        try
        {
            this.fixture.dao.Delete(entity);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.Null(exception);
    }

    [Fact]
    public void IHopeToGet()
    {
        Exception exception = null;
        this.fixture.dao.Insert(this.entity);
        var found = this.fixture.dao.Get(this.entity);
        Assert.Equal(found[0].Id, this.entity.Id);
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
