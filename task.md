# Deployment Tasks

## Issue Fixed: Docker Compose Build Error

**Problem:** `failed to read dockerfile: open Dockerfile: no such file or directory`

**Solution:** Make sure you're running the command from the correct directory.

### ✅ Correct Deployment Steps:

#### 1. Navigate to project root:
```bash
cd /path/to/DataPlatform  # Make sure you're in the folder with Dockerfile
ls -la                    # Verify Dockerfile exists
```

#### 2. Deploy with Internal Database:
```bash
sudo docker compose --profile internaldb up -d --build
```

#### 3. Deploy with External Database:
```bash
export USE_INTERNAL_DB=false
sudo docker compose up -d --build web
```

#### 4. Check Status:
```bash
sudo docker compose ps
sudo docker compose logs web
```

### 📍 URLs After Deployment:
- **App**: http://192.168.29.217:8080/app
- **API**: http://192.168.29.217:8080/app/api/languages  
- **Docs**: http://192.168.29.217:8080/app/scalar

### 🔧 Troubleshooting:
- Ensure you're in the directory containing `Dockerfile`
- Run `ls` to verify `Dockerfile` and `docker-compose.yml` exist
- Use `sudo` if you get permission errors
- Check logs with `sudo docker compose logs`

## Status: ✅ RESOLVED

