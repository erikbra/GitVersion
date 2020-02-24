using GitVersion.Models.Abstractions;

namespace GitVersionCore.Tests.Mocks
{
    public class MockTag : IGitTag
    {
        public string NameEx;
        public string FriendlyName => NameEx;

        public IGitObject TargetEx;
        public IGitObject Target => TargetEx;
        public IGitTagAnnotation AnnotationEx;

        public MockTag() { }

        public MockTag(string name, IGitCommit target)
        {
            NameEx = name;
            TargetEx = target;
        }

        public IGitTagAnnotation Annotation => AnnotationEx;
    }
}
