using NuGet.ContentModel;
using Project_Forum.Models;
namespace ForumUnitTests
{
    [TestClass]
    public class TagExtractorUnitTest
    {
        [DataTestMethod]
        [DataRow("#tag #tag", "tag")]
        [DataRow("#Tag #tag", "Tag tag")]
        [DataRow("#tag #tag1", "tag tag1")]
        [DataRow("#tag #tag1 tag", "tag tag1")]
        // second DataRow parameter should be given with space in between expected tags
        public void ExtractTags_Duplicates(string content, string expectedTagsString)
        {
            HashSet<string> expectedTags = new HashSet<string>(expectedTagsString.Split(' '));

            HashSet<string> actualTags = TagExtractor.ExtractTags(content);

            Assert.IsTrue(expectedTags.SetEquals(actualTags));
        }


        [DataTestMethod]
        [DataRow("#tag#tag #tag2", "tag2")]
        [DataRow("#Tag#tag #tag2", "tag2")]
        [DataRow("#tag#tag1 #tag #tag1", "tag tag1")]
        [DataRow("#tag#tag1#tag #tag2", "tag2")]
        // second DataRow parameter should be given with space in between expected tags
        public void ExtractTags_NoSpaceInBetween(string content, string expectedTagsString)
        {
            HashSet<string> expectedTags = new HashSet<string>(expectedTagsString.Split(' '));

            HashSet<string> actualTags = TagExtractor.ExtractTags(content);

            
            Assert.IsTrue(expectedTags.SetEquals(actualTags));
        }


       

        [DataTestMethod]
        [DataRow("    #tag2   ", "tag2")]
        [DataRow("#tag2   ", "tag2")]
        [DataRow("    #tag2", "tag2")]
        [DataRow("#Tag   #tag   #tag2", "Tag tag tag2")]
        [DataRow("   #Tag   #tag   #tag2", "Tag tag tag2")]
        [DataRow("#Tag   #tag   #tag2  ", "Tag tag tag2")]
        // second DataRow parameter should be given with space in between expected tags
        public void ExtractTags_MultipleSpaces(string content, string expectedTagsString)
        {
            HashSet<string> expectedTags = new HashSet<string>(expectedTagsString.Split(' '));

            HashSet<string> actualTags = TagExtractor.ExtractTags(content);


            Assert.IsTrue(expectedTags.SetEquals(actualTags));
        }

        [DataTestMethod]
        [DataRow("#tag1! #tag2", "tag2")]
        [DataRow("#t@g1 #tag2", "tag2")]
        [DataRow("#!Tag   #tag2 ", "tag2")]
        [DataRow("#!Tag   #tag2 ", "tag2")]
        // second DataRow parameter should be given with space in between expected tags
        public void ExtractTags_SpecialCharacters(string content, string expectedTagsString)
        {
            HashSet<string> expectedTags = new HashSet<string>(expectedTagsString.Split(' '));

            HashSet<string> actualTags = TagExtractor.ExtractTags(content);


            Assert.IsTrue(expectedTags.SetEquals(actualTags));
        }

        [TestMethod]
        public void ExtractTags_NoTags()
        {
            HashSet<string>? actualTags = TagExtractor.ExtractTags("text text loremipsutm no tags notags");

            Assert.IsTrue(actualTags.Count == 0);

        }
        [TestMethod]
        public void ExtractTags_NullContent()
        {
            string? s = null;

            HashSet<string> actualTags = TagExtractor.ExtractTags(s);

            Assert.IsNotNull(actualTags);
            Assert.IsTrue(actualTags.Count == 0);
        }
        [TestMethod]
        public void ExtractTags_EmptyContent()
        {
            string s = "";

            HashSet<string> actualTags = TagExtractor.ExtractTags(s);

            Assert.IsNotNull(actualTags);
            Assert.IsTrue(actualTags.Count == 0);
        }
        [TestMethod]
        public void ExtractTags_TagExceedesMaxNum()
        {
            string s = "#Loremipsumdolorsitametconsectetueradipiscing1111111";

            HashSet<string> actualTags = TagExtractor.ExtractTags(s);

            Assert.IsNotNull(actualTags);
            Assert.IsTrue(actualTags.Count == 0);
        }
    }
}