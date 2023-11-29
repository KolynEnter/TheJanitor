using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using CS576.Janitor.Trashes;
using CS576.Janitor.Process;

public class ContainerTester
{
    private Container<Trash> _container = new Container<Trash>(5);

    [Test]
    public void TestInitialization()
    {
        Assert.IsNull(_container.GetElement(0));
        Assert.IsNull(_container.GetElement(1));
        Assert.IsNull(_container.GetElement(2));
        Assert.IsNull(_container.GetElement(3));
        Assert.IsNull(_container.GetElement(4));
    }

    [Test]
    public void TestIncrement()
    {
        Trash trash = ScriptableObject.CreateInstance<Trash>();
        _container.Increment(trash);
        _container.Increment(trash);

        Assert.IsNotNull(_container.GetElement(0));
        Assert.IsNull(_container.GetElement(1));
        Assert.IsNull(_container.GetElement(2));
        Assert.IsNull(_container.GetElement(3));
        Assert.IsNull(_container.GetElement(4));

        Assert.AreEqual(_container.GetNumberOf(trash), 2);
    }

    [Test]
    public void TestDecrement()
    {
        Trash trash = ScriptableObject.CreateInstance<Trash>();
        _container.Decrement(trash);

        Assert.IsNull(_container.GetElement(0));
        Assert.IsNull(_container.GetElement(1));
        Assert.IsNull(_container.GetElement(2));
        Assert.IsNull(_container.GetElement(3));
        Assert.IsNull(_container.GetElement(4));

        _container.Increment(trash);
        _container.Increment(trash);
        _container.Decrement(trash);
        Assert.AreEqual(_container.GetNumberOf(trash), 1);
    }

    [Test]
    public void TestIncrement2()
    {
        Trash trash1 = ScriptableObject.CreateInstance<Trash>();
        Trash trash2 = ScriptableObject.CreateInstance<Trash>();

        _container.Increment(trash1);
        _container.Increment(trash2);

        Assert.IsNotNull(_container.GetElement(0));
        Assert.IsNotNull(_container.GetElement(1));
        Assert.IsNull(_container.GetElement(2));
        Assert.IsNull(_container.GetElement(3));
        Assert.IsNull(_container.GetElement(4));
    }

    [Test]
    public void TestDecrement2()
    {
        Trash trash1 = ScriptableObject.CreateInstance<Trash>();
        Trash trash2 = ScriptableObject.CreateInstance<Trash>();

        _container.Increment(trash1);
        _container.Increment(trash2);
        _container.Decrement(trash1);

        Assert.IsNull(_container.GetElement(0));
        Assert.IsNotNull(_container.GetElement(1));
        Assert.IsNull(_container.GetElement(2));
        Assert.IsNull(_container.GetElement(3));
        Assert.IsNull(_container.GetElement(4));
        Assert.AreEqual(_container.GetNumberOf(trash2), 1);
    }
}
