# Paozinhos da Vovo - Postman Collection

This is a Postman collection named "Paozinhos da Vovo" used to performs http requests to communicate with [Helix Sandbox](https://github.com/Helix-Platform/Sandbox-NG).

## Collection 

[Json Collection](/postman/vovo.postman_collection.json)

## Requests

### 1. Health Check

- **Method**: GET
- **URL**: `http://{{url}}:4041/iot/about`

### 2. Provisioning a Service Group for MQTT

- **Method**: POST
- **URL**: `http://{{url}}:4041/iot/services`
- **Headers**:
  - `Content-Type: application/json`
  - `fiware-service: helixiot`
  - `fiware-servicepath: /`
- **Body**:
  ```json
  {
    "services": [
      {
        "apikey": "iot",
        "cbroker": "http://{{url}}:1026",
        "entity_type": "Thing",
        "resource": ""
      }
    ]
  }
  ```

### 3. List all Devices Provisioned

- **Method**: GET
- **URL**: `http://{{url}}:4041/iot/devices`
- **Headers**:
  - `fiware-service: helixiot`
  - `fiware-servicepath: /`

### 4. Provisioning an Object

- **Method**: POST
- **URL**: `http://{{url}}:4041/iot/devices`
- **Headers**:
  - `Content-Type: application/json`
  - `fiware-service: helixiot`
  - `fiware-servicepath: /`
- **Body**:
  ```json
  {
    "devices": [
      {
        "device_id": "espcam001",
        "entity_name": "urn:ngsi-ld:Espcam:001",
        "entity_type": "Espcam",
        "protocol": "PDI-IoTA-UltraLight",
        "transport": "MQTT",
        "commands": [
          {
            "name": "ok",
            "type": "command"
          },
          {
            "name": "nok",
            "type": "command"
          }
        ],
        "attributes": [
          {
            "object_id": "s",
            "name": "qr_code",
            "type": "Text"
          }
        ]
      }
    ]
  }
  ```

### 5. Registering Commands

- **Method**: POST
- **URL**: `http://{{url}}:1026/v2/registrations`
- **Headers**:
  - `Content-Type: application/json`
  - `fiware-service: helixiot`
  - `fiware-servicepath: /`
- **Body**:
  ```json
  {
    "description": "Espcam Commands",
    "dataProvided": {
      "entities": [
        {
          "id": "urn:ngsi-ld:Espcam:001",
          "type": "Espcam"
        }
      ],
      "attrs": ["ok", "nok"]
    },
    "provider": {
      "http": {"url": "http://{{url}}:4041"},
      "legacyForwarding": true
    }
  }
  ```

### 6. Switching on the Espcam

- **Method**: PATCH
- **URL**: `http://{{url}}:1026/v2/entities/urn:ngsi-ld:Espcam:001/attrs`
- **Headers**:
  - `Content-Type: application/json`
  - `fiware-service: helixiot`
  - `fiware-servicepath: /`
- **Body**:
  ```json
  {
    "ok": {
      "type": "command",
      "value": ""
    }
  }
  ```

### 7. Result of Espcam

- **Method**: GET
- **URL**: `http://{{url}}:1026/v2/entities/urn:ngsi-ld:Espcam:001/attrs/qr_code`
- **Headers**:
  - `fiware-service: helixiot`
  - `fiware-servicepath: /`
  - `accept: application/json`

Replace `{{url}}` to the IP number of the network used in the connection with the [Helix Sandbox](https://github.com/Helix-Platform/Sandbox-NG).