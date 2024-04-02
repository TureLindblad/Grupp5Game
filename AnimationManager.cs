﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Grupp5Game
{
    public class AnimationManager // kanske inte behövs
    {
        public Animation _animation;
        private float _timer;
        public System.Numerics.Vector2 Position {  get;  set; }
        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_animation.Texture,
                                Position,
                                new Rectangle(_animation.CurrentFrame * _animation.FrameWidth,
                                              0,
                                              _animation.FrameWidth,
                                              _animation.FrameHeight),
                                Color.White);

        }

        /*public void DrawCustomAnimation(SpriteBatch spriteBatch, Rectangle[] customFrames) 
        {
            Rectangle currentFrames = customFrames[_animation.CurrentFrame % customFrames.Length];
            spriteBatch.Draw(_animation.Texture, Position, currentFrames, Color.White);
        }*/
        public void Play(Animation animation) 
        {
            if(_animation == animation)
                return;
            _animation = animation;
            _animation.CurrentFrame = 0;
            _timer = 0;
        }
        public void Stop() 
        {
            _timer  = 0f;
            _animation.CurrentFrame = 0;
        }
        public void Update(GameTime gameTime )
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_timer > _animation.FrameSpeed)
            {
                _timer = 0f;
                _animation.CurrentFrame++;
                if(_animation.CurrentFrame >= _animation.FrameCount)
                {
                    _animation.CurrentFrame = 0;
                }
            }
        }
    }
}
