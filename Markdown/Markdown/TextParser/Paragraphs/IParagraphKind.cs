using Markdown.Tree;

namespace Markdown.TextParser.Paragraphs
{
    internal interface IParagraphKind
    {
        //Не нужно указывать в имени функции, что она может вернуть null. 
        //Если по каким-то причинам тебе нужно это подчеркнуть, то существует специальный атрибут в JetBrains.Annotations.
        //[CanBeNull]
        INode ParseOrNull(string str, bool wrap);
    }
}