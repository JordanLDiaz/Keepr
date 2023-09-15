#! /bin/bash
set -e
set -x

export NVM_DIR="$HOME/.nvm"
[ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh"  # This loads nvm
[ -s "$NVM_DIR/bash_completion" ] && \. "$NVM_DIR/bash_completion"  # This loads nvm bash_completion

SCRIPTPATH="$( cd -- "$(dirname "$0")" >/dev/null 2>&1 ; pwd -P )"

# Go to top level directory
cd ${SCRIPTPATH}/..

# Grab the latest changes
git pull

# rebuild client
cd Keepr.client
npm run build

# rebuild server
cd ../Keepr
dotnet build
dotnet publish --configuration Release

# redo systemd configuration
cd ../deploy
cp Keepr.service /etc/systemd/system/Keepr.service
systemctl daemon-reload
systemctl restart Keepr.service

# Not sure I really want to manually update nginx, not sure how that will play
# with certbot automatically renewing our nginx certificates.
#cp nginx.default /etc/nginx/sites-available/default

