using Draw;
using Logic;
using Moq;
using NUnit.Framework;
using Settings;
using UnityEngine;

public class GameOver_Tests
{
    [Test]
    public void Show_label_game_over_when_intersection_to_body()
    {
        bool checkIsShowGameOver = false;
        
        //Snake
        var mSnake = new Mock<ISnake>();
        mSnake.Setup(x => x.IsIntersectionToBody()).Returns(true);

        //UiDraw
        var mUiDraw = new Mock<IUiDraw>();
        mUiDraw.Setup(x => x.ShowGameOver()).
            Callback(() => checkIsShowGameOver = true);
        
        //Test
        var gameOverHandler 
            = new GameOverHandler(new MapSettings() , mSnake.Object , mUiDraw.Object);
        
        mSnake.Raise(x => x.OnAfterStep += null);
        
        Assert.IsTrue(checkIsShowGameOver);
    }

    private bool CheckGameOverByIntersectionToBolder(Vector2Int mapSize , Vector2Int pointHead)
    {
        bool checkIsShowGameOver = false;

        var mapSetting = new MapSettings
        {
            MapSize = mapSize
        };
        
        //Snake
        var mSnake = new Mock<ISnake>();
        mSnake.Setup(x => x.GetPositionHead()).Returns(new Point(pointHead.x, pointHead.y));

        //UiDraw
        var mUiDraw = new Mock<IUiDraw>();
        mUiDraw.Setup(x => x.ShowGameOver()).
            Callback(() => checkIsShowGameOver = true);
        
        //Test
        var gameOverHandler 
            = new GameOverHandler(mapSetting , mSnake.Object , mUiDraw.Object);
        
        mSnake.Raise(x => x.OnAfterStep += null);

        return checkIsShowGameOver;
    }
    
    [Test]
    public void Show_label_game_over_when_intersection_to_bolder()
    {
        var mapSize = new Vector2Int(10, 10);
        
        Assert.IsFalse(CheckGameOverByIntersectionToBolder(mapSize, new Vector2Int(8,8)));
        Assert.IsFalse(CheckGameOverByIntersectionToBolder(mapSize, new Vector2Int(5,5)));
        Assert.IsTrue(CheckGameOverByIntersectionToBolder(mapSize, new Vector2Int(20,20)));
        Assert.IsTrue(CheckGameOverByIntersectionToBolder(mapSize, new Vector2Int(0,0)));
    }
}