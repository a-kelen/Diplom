<template>
  <v-container>
    <v-row>
        <v-col md="3">
            <v-text-field
                v-model="item.name"
                label="Event name"
                id="id"
                filled
                outlined
                rounded
            />
        </v-col>
        <v-col>
            <v-textarea 
            v-model="item.description"
            label="Event description"
            rows="1"
            auto-grow
            filled/>
        </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: 'AddEventItem',
  props: ['item'],
  data: () => ({
  })
};
</script>
