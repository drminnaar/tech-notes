docker exec -it kafka kafka-topics --create \
  --bootstrap-server localhost:9092 \
  --topic customer.product-reviews.v1 \
  --partitions 3 \
  --replication-factor 1

cat example-3.json | \
  docker exec -i kafka kafka-console-producer \
  --bootstrap-server localhost:9092 \
  --topic customer.product-reviews.v1 \
  --property "parse.key=true" \
  --property "key.separator=:"