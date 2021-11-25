using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using ObjectPrinting;
using ObjectPrinting.Extensions;

namespace ObjectPrintingTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AcceptanceTest()
        {
            var person = new Person { Name = "Alex", Age = 19, Height = 170.13};

            var printer = ObjectPrinter.For<Person>()
                //1. ��������� �� ������������ �������� ������������� ����
                .Excluding<Guid>()
                //2. ������� �������������� ������ ������������ ��� ������������� ����
                .Printing<int>().Using(i => i.ToString("X"))
                //3. ��� �������� ����� ������� ��������
                .Printing<double>().Using(CultureInfo.InvariantCulture)
                //4. ��������� ������������ ����������� ��������
                .Printing(x => x.Height).Using(i => i.ToString("P"))
                //5. ��������� ��������� ��������� ������� (����� ������ ���� ����� ������ ��� ��������� �������)
                .Printing(p => p.Name).TrimmedToLength(10)
                //6. ��������� �� ������������ ����������� ��������
                .Excluding(p => p.Age);

            var s1 = printer.PrintToString(person);

            //7. �������������� ����� � ���� ������ ����������, �������������� ��-���������
            var s2 = person.PrintToString();

            //8. ...� �����������������
            var s3 = person.PrintToString(s => s.Excluding(p => p.Age).Excluding(p => p.Name));
            Console.WriteLine(s1);
            Console.WriteLine(s2);
            Console.WriteLine(s3);
        }

        [Test]
        public void Test()
        {
            var person = new Person { Name = "Alex", Age = 19, Height = 170.13 };
            var dictionary = new Dictionary<string, Person>
            {
                {"a", person},
                {"b", person}
            };
            var str = dictionary.PrintToString();
            Console.WriteLine(str);
        }
    }
}