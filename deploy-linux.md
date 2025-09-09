# Deploy Your App to Linux - ACTUAL Simple Steps

## What You Need:
- Your Linux VM IP: `192.168.29.217` ✅
- SSH access to Linux VM
- 10 minutes

## ✅ NETWORK STATUS: Good!
Your IP `192.168.29.217` is bridged network - perfect for web access!

## Step 1: Build Docker Image (On Windows)
```powershell
cd C:\Ashvad\POC_VSCode\DataPlatform
docker build -t dataplatform-app .
```

## Step 2: Save Image to File
```powershell
docker save dataplatform-app -o dataplatform-app.tar
```

## Step 2.1: Install OpenSSH server

# Update package lists
sudo apt update

# Install OpenSSH server
sudo apt install -y openssh-server

# Enable and start the service
sudo systemctl enable ssh
sudo systemctl start ssh

# Check status
systemctl status ssh


## Step 3: Copy to Linux VM
```powershell
# Your specific command:
scp dataplatform-app.tar lin@192.168.29.217:/home/lin/
scp dataplatform-app.tar docker-compose.yml .env lin@192.168.29.217:/home/lin/ 
```

## Step 4: Install Docker on Linux (Run once only)
```bash
sudo apt update
sudo apt install docker.io -y #Install Docker
sudo systemctl start docker #Start Docker
sudo systemctl enable docker #Enable auto-start at boot:
sudo usermod -aG docker $USER #Fix permissions
newgrp docker #his makes docker usable without sudo.

# Your specific command for docker compose: (if needed)
sudo apt install -y ca-certificates curl gnupg
sudo install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] \
  https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt update

sudo apt install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

docker compose version


```
**Important: Log out and log back in after this step**

## Step 5: Load and Run on Linux
```bash
# Load the image
docker load -i dataplatform-app.tar

# Run the container
docker compose --profile internaldb up -d
docker run -d -p 8080:80 --name myapp dataplatform-app

# Check if running
docker ps
```

## Step 6: Open Firewall
```bash
sudo ufw allow 8080
sudo ufw allow 5432/tcp
```

## Access Your App
Open browser: `http://192.168.29.217:8080`

Perfect! This IP will work from your Windows machine.

## If Something Goes Wrong:
```bash
# Check container logs
docker logs myapp

# Stop container
docker stop myapp

# Remove container
docker rm myapp

# Run again
docker run -d -p 8080:80 --name myapp dataplatform-app
```

That's it. No scripts, no complexity. Just copy-paste these commands.
