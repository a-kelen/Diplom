<template>
  <router-link :to="{ name: 'ComponentPage', params: {id: component.id} }">
    <v-card class="d-flex align-center my-2 pa-2">
        <v-icon color="green">mdi-vuejs</v-icon>
        <div class="mx-3">ComponentName</div>
        <v-chip color="green" class="ml-auto" label>Format</v-chip>
    </v-card>
  </router-link>
</template>

<script>
export default {
  name: 'ComponentRow',
  data: () => ({
  }),
  props: ['component']
};
</script>
