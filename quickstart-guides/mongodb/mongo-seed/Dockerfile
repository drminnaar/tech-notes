FROM mongo:8-noble
ENV LAB_MONGO_INITDB_ROOT_USERNAME=""
ENV LAB_MONGO_INITDB_ROOT_PASSWORD=""
COPY mflix.gz mflix.gz
CMD mongorestore --host=mongo --port=27017 --username="$MONGO_ROOT_USERNAME" --password="$MONGO_ROOT_PASSWORD" --nsFrom="sample_mflix.*" --nsTo="mflix.*" --gzip --archive=mflix.gz