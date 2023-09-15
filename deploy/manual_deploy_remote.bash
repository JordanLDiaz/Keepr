#! /bin/bash
set -e   # quit on errors
set -x   # show step by step

### This script is intended to be run /locally/, and it should deploy tower on the remote machine
ssh droplet "cd /var/www/Keepr/ && git pull"
ssh droplet "/var/www/Keepr/deploy/manual_deploy.bash"

