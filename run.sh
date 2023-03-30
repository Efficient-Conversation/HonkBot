#!/bin/bash
rm -rf HonkBot
git clone -b main https://github.com/Efficient-Conversation/HonkBot
cd HonkBot || exit

tmux new -d -s honkbot 'cd /home/$USER/HonkBot/; bash run_honkbot.sh > /home/$USER/honkbot-log.txt 2>&1'
#tmux new -d -s lavalink 'cd /home/$USER/HonkBot/; bash run_lavalink.sh > /home/$USER/lavalink-log.txt 2>&1'
