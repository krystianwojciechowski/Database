using Xunit;
using Bogus;
using Database;
using DatabaseTest.TestEntities;
using DatabaseTest.Fixtures;
using System;

namespace DatabaseTest;

public class MongoDBTest : IClassFixture<MongoDBFixture>
{
    private readonly MongoDBFixture fixture;
    private MongoDBTestEntity entity;
    private Faker faker;

    public MongoDBTest(MongoDBFixture fixture)
    {
        this.fixture = fixture;
        this.faker = new Faker();
        this.entity = new MongoDBTestEntity()
        {
            Name = faker.Name.FirstName(),
            Text = faker.Rant.Review()
        };
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
        this.entity.Text = faker.Rant.Review();

        try
        {
            this.fixture.dao.Update(entity);
        }
        catch (Exception e)
        {
            exception = e;
        }

        Assert.Null(exception);
    }


    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
