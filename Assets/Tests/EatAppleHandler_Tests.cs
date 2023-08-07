using Draw;
using Logic;
using Moq;
using NUnit.Framework;
using Settings;

public class EatAppleHandler_Tests
{
    private bool CheckIsChangeScoreByEatApple(Point pointApple, Point pointHead)
    {
        int score = 0;
        
        //Snake
        var mSnake = new Mock<ISnake>();
        mSnake.Setup(x => x.GetPositionHead()).Returns(pointHead);
        mSnake.Setup(x => x.IsIntersection(It.IsAny<Point>())).
            Returns<Point>((point) => point == pointHead);

        //UiDraw
        var mUiDraw = new Mock<IUiDraw>();
        mUiDraw.Setup(x => x.SetScore(It.IsAny<int>())).
            Callback(() => score++);
        
        //Apple
        var mApple = new Mock<IApple>();
        mApple.SetupGet(x => x.Position).Returns(pointApple);
        
        //Test
        EatAppleHandler eatAppleHandler = new EatAppleHandler(new MapSettings() , 
            mSnake.Object , mApple.Object , mUiDraw.Object);
        
        mSnake.Raise(x => x.OnAfterStep += null);

        return score > 0;
    }
    
    [Test]
    public void Change_score_when_eat_apple()
    {
        Assert.IsTrue(CheckIsChangeScoreByEatApple(new Point(5, 5) , new Point(5,5)) , "Score is not change");
    }
}