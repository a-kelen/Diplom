<template>
  <v-container>
    <v-row>
        <v-col md="3">
            <v-text-field
                v-model="item.name"
                label="Prop name"
                filled
                outlined
                rounded
            />
        </v-col>
        <v-col>
            <v-textarea 
            v-model="item.description"
            label="Prop description"
            rows="1"
            auto-grow
            filled/>
        </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: 'AddPropItem',
  props: ['item'],
  data: () => ({
  })
};
</script>
